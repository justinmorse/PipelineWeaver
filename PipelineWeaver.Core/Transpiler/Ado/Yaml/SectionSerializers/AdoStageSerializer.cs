using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoStageSerializer : IAdoYamlSectionSerializer
    {
        public AdoYamlBuilder _builder = new AdoYamlBuilder();

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var stage = section as AdoStageContainer ?? throw new ArgumentException(nameof(section));
            if (builder is not null)
                _builder = builder;

            if (stage.Stages?.Count > 0)
            {
                _builder.AppendLine(startingIndent, "stages:");
                AppendStages(stage, startingIndent + 2);
            }
        }

        private void AppendStages(AdoStageContainer stages, int startingIndent)
        {
            if (stages.Stages?.Count > 0)
            {
                stages.Stages.ForEach(s =>
                {
                    switch (s)
                    {
                        case AdoStage stage:
                            {
                                AppendStage(stage, startingIndent);
                                break;
                            }
                        case AdoStageTemplate stage:
                            {
                                AppendStageTemplate(stage, startingIndent);
                                break;
                            }
                        default:
                            throw new ArgumentException(nameof(s));
                    }
                });
            }
        }

        private void AppendStageTemplate(AdoStageTemplate stage, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- template: " + stage.Template);
            if (stage.Parameters?.Count > 0)
            {
                _builder.AppendList("parameters", 2 + startingIndent, stage.Parameters);
            }
            AppendBaseFields(stage, startingIndent);
        }

        private void AppendStage(AdoStage stage, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- stage: " + stage.Stage);
            AppendBaseFields(stage, startingIndent);
            if (!string.IsNullOrWhiteSpace(stage.DisplayName))
                _builder.AppendLine(startingIndent + 2, "displayName: " + stage.DisplayName);
            if (stage.Variables is not null)
                _builder.Append(stage.Variables, startingIndent);
            if (!string.IsNullOrWhiteSpace(stage.LockBehavior))
                _builder.AppendLine(startingIndent + 2, "lockBehavior: " + stage.LockBehavior);
            if (!string.IsNullOrWhiteSpace(stage.Trigger))
                _builder.AppendLine(startingIndent + 2, "trigger: " + stage.Trigger);
            if (stage.IsSkippable is not null)
                _builder.AppendLine(startingIndent + 2, "isSkippable: " + stage.IsSkippable.Value);
            if (!string.IsNullOrWhiteSpace(stage.TemplateContext))
                _builder.AppendLine(startingIndent + 2, "templateContext: " + stage.TemplateContext);
            if (stage.Pools is not null)
                _builder.Append(stage.Pools, startingIndent);
            if (stage.Jobs?.Jobs.Count > 0)
                _builder.Append(stage.Jobs, startingIndent);

        }

        private void AppendBaseFields(AdoStageBase stage, int startingIndent)
        {
            if (stage.DependsOn?.Count > 0)
                _builder.AppendList("dependsOn", 2 + startingIndent, stage.DependsOn);
            if (string.IsNullOrEmpty(stage.Condition))
                _builder.AppendLine(startingIndent + 2, "condition: " + stage.Condition);
        }
    }
}