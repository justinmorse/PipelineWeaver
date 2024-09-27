using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoTriggerSerializer : IAdoYamlSectionSerializer
    {
        internal AdoYamlBuilder _builder = new AdoYamlBuilder();
        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var trigger = section as AdoTriggerContainer ?? throw new ArgumentException(nameof(section));
            if (builder is not null)
                _builder = builder;

            _builder.AppendLine(startingIndent, "triggers:");
            if (trigger.Triggers.Count > 0)
            {
                if (trigger.Batch)
                    _builder.AppendLine(startingIndent + 2, "batch: true");
                AppendBranchTriggers(trigger, startingIndent + 2);
                AppendPathTriggers(trigger, startingIndent + 2);
                AppendTagTriggers(trigger, startingIndent + 2);
            }
        }

        private void AppendTagTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var tagTriggers = triggers.Triggers.Where(t => t.TriggerType == AdoTriggerType.TagInclude || t.TriggerType == AdoTriggerType.TagExclude);
            if (tagTriggers.Count() == 0)
                return;

            _builder.AppendLine(startingIndent, "tags:");
            if (tagTriggers.Any(t => t.TriggerType == AdoTriggerType.TagInclude))
                _builder.AppendList("include", 2 + startingIndent, tagTriggers.Where(t => t.TriggerType == AdoTriggerType.TagInclude).Select(t => t.Value).ToList());
            if (tagTriggers.Any(t => t.TriggerType == AdoTriggerType.TagExclude))
                _builder.AppendList("exclude", 2 + startingIndent, tagTriggers.Where(t => t.TriggerType == AdoTriggerType.TagExclude).Select(t => t.Value).ToList());
        }

        private void AppendPathTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var pathTriggers = triggers.Triggers.Where(t => t.TriggerType == AdoTriggerType.PathInclude || t.TriggerType == AdoTriggerType.PathExclude);
            if (pathTriggers.Count() == 0)
                return;

            _builder.AppendLine(startingIndent, "paths:");
            if (pathTriggers.Any(t => t.TriggerType == AdoTriggerType.PathInclude))
                _builder.AppendList("include", 2 + startingIndent, pathTriggers.Where(t => t.TriggerType == AdoTriggerType.PathInclude).Select(t => t.Value).ToList());
            if (pathTriggers.Any(t => t.TriggerType == AdoTriggerType.PathExclude))
                _builder.AppendList("exclude", 2 + startingIndent, pathTriggers.Where(t => t.TriggerType == AdoTriggerType.PathExclude).Select(t => t.Value).ToList());
        }

        private void AppendBranchTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var branchTriggers = triggers.Triggers.Where(t => t.TriggerType == AdoTriggerType.BranchInclude || t.TriggerType == AdoTriggerType.BranchExclude);
            if (branchTriggers.Count() == 0)
                return;

            _builder.AppendLine(startingIndent, "branches:");
            if (branchTriggers.Any(t => t.TriggerType == AdoTriggerType.BranchInclude))
                _builder.AppendList("include", 2 + startingIndent, branchTriggers.Where(t => t.TriggerType == AdoTriggerType.BranchInclude).Select(t => t.Value).ToList());
            if (branchTriggers.Any(t => t.TriggerType == AdoTriggerType.BranchExclude))
                _builder.AppendList("exclude", 2 + startingIndent, branchTriggers.Where(t => t.TriggerType == AdoTriggerType.BranchExclude).Select(t => t.Value).ToList());
        }
    }
}