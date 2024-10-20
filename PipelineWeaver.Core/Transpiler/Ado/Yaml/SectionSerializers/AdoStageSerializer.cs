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
        private AdoYamlBuilder _builder = new AdoYamlBuilder();

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var stage = section as AdoSectionCollection<AdoStageBase> ?? throw new ArgumentException(nameof(section));

            if (stage.Count > 0)
            {
                _builder.AppendLine(0, "stages:");
                AppendStages(stage);
                builder.AppendLine(startingIndent, _builder.ToString(), true, true);
            }
        }

        private void AppendStages(AdoSectionCollection<AdoStageBase> stages)
        {
            if (stages?.Count > 0)
            {
                stages.ToList().ForEach(s =>
                {
                    switch (s)
                    {
                        case AdoStage stage:
                            {
                                AppendStage(stage);
                                break;
                            }
                        case AdoStageTemplate stage:
                            {
                                AppendStageTemplate(stage);
                                break;
                            }
                        default:
                            throw new ArgumentException(nameof(s));
                    }
                });
            }
        }

        private void AppendStageTemplate(AdoStageTemplate stage)
        {
            _builder.AppendLine(0, "- template: " + stage.Template);
            if (stage.Parameters?.Count > 0)
            {
                _builder.Append(2, stage.Parameters);
            }
            AppendBaseFields(stage);
        }

        private void AppendStage(AdoStage stage)
        {
            _builder.AppendLine(0, "- stage: " + stage.Stage);
            AppendBaseFields(stage);
            if (!string.IsNullOrWhiteSpace(stage.DisplayName))
                _builder.AppendLine(2, "displayName: " + stage.DisplayName);
            if (stage.Variables?.Count > 0)
                _builder.Append(2, stage.Variables);
            if (!string.IsNullOrWhiteSpace(stage.LockBehavior))
                _builder.AppendLine(2, "lockBehavior: " + stage.LockBehavior);
            if (!string.IsNullOrWhiteSpace(stage.Trigger))
                _builder.AppendLine(2, "trigger: " + stage.Trigger);
            if (stage.IsSkippable is not null)
                _builder.AppendLine(2, "isSkippable: " + stage.IsSkippable.Value);
            if (!string.IsNullOrWhiteSpace(stage.TemplateContext))
                _builder.AppendLine(2, "templateContext: " + stage.TemplateContext);
            if (stage.Pools is not null)
                _builder.Append(2, stage.Pools);
            if (stage.Jobs?.Count > 0)
                _builder.Append(2, stage.Jobs);

        }

        private void AppendBaseFields(AdoStageBase stage)
        {
            if (stage.DependsOn?.Count > 0)
                _builder.AppendArray("dependsOn", 2, stage.DependsOn.ToArray());
            if (string.IsNullOrEmpty(stage.Condition))
                _builder.AppendLine(2, "condition: " + stage.Condition);
        }
    }
}