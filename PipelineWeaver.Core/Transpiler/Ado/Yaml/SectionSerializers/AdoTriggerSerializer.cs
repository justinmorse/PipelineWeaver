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

            _builder.AppendLine(0, "triggers:");
            if (trigger.Count > 0)
            {
                if (trigger.Batch)
                    _builder.AppendLine(2, "batch: true");
                AppendBranchTriggers(trigger, 2);
                AppendPathTriggers(trigger, 2);
                AppendTagTriggers(trigger, 2);
                builder.AppendLine(startingIndent, _builder.ToString(), true, true);
            }
        }

        private void AppendTagTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var tagTriggers = triggers.Where(t => t.TriggerType == AdoTriggerType.TagInclude || t.TriggerType == AdoTriggerType.TagExclude);
            if (!tagTriggers.Any())
                return;

            _builder.AppendLine(startingIndent, "tags:");
            if (tagTriggers.Any(t => t.TriggerType == AdoTriggerType.TagInclude))
                _builder.AppendArray("include", 2 + startingIndent, tagTriggers.Where(t => t.TriggerType == AdoTriggerType.TagInclude).Select(t => t.Value).ToArray());
            if (tagTriggers.Any(t => t.TriggerType == AdoTriggerType.TagExclude))
                _builder.AppendArray("exclude", 2 + startingIndent, tagTriggers.Where(t => t.TriggerType == AdoTriggerType.TagExclude).Select(t => t.Value).ToArray());
        }

        private void AppendPathTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var pathTriggers = triggers.Where(t => t.TriggerType == AdoTriggerType.PathInclude || t.TriggerType == AdoTriggerType.PathExclude);
            if (!pathTriggers.Any())
                return;

            _builder.AppendLine(startingIndent, "paths:");
            if (pathTriggers.Any(t => t.TriggerType == AdoTriggerType.PathInclude))
                _builder.AppendArray("include", 2 + startingIndent, pathTriggers.Where(t => t.TriggerType == AdoTriggerType.PathInclude).Select(t => t.Value).ToArray());
            if (pathTriggers.Any(t => t.TriggerType == AdoTriggerType.PathExclude))
                _builder.AppendArray("exclude", 2 + startingIndent, pathTriggers.Where(t => t.TriggerType == AdoTriggerType.PathExclude).Select(t => t.Value).ToArray());
        }

        private void AppendBranchTriggers(AdoTriggerContainer triggers, int startingIndent)
        {
            var branchTriggers = triggers.Where(t => t.TriggerType == AdoTriggerType.BranchInclude || t.TriggerType == AdoTriggerType.BranchExclude);
            if (!branchTriggers.Any())
                return;

            _builder.AppendLine(startingIndent, "branches:");
            if (branchTriggers.Any(t => t.TriggerType == AdoTriggerType.BranchInclude))
                _builder.AppendArray("include", 2 + startingIndent, branchTriggers.Where(t => t.TriggerType == AdoTriggerType.BranchInclude).Select(t => t.Value).ToArray());
            if (branchTriggers.Any(t => t.TriggerType == AdoTriggerType.BranchExclude))
                _builder.AppendArray("exclude", 2 + startingIndent, branchTriggers.Where(t => t.TriggerType == AdoTriggerType.BranchExclude).Select(t => t.Value).ToArray());
        }
    }
}