using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoStepSerializer : IAdoYamlSectionSerializer
    {
        internal AdoYamlBuilder _builder = new AdoYamlBuilder();

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent)
        {
            var stepSection = section as AdoSectionCollection<AdoStepBase> ?? throw new ArgumentException(nameof(section));

            _builder.AppendLine(0, "steps:");
            AppendSteps(stepSection);

            builder.AppendLine(startingIndent, _builder.ToString(), true, true);
        }


        public void AppendSteps(AdoSectionCollection<AdoStepBase> steps)
        {
            foreach (var s in steps)
            {
                switch (s)
                {
                    case AdoScriptStep step:
                        AppendScriptStep(step);
                        break;
                    case AdoTaskStep step:
                        AppendTaskStep(step);
                        break;
                    case AdoBashStep step:
                        AppendBashStep(step);
                        break;
                    case AdoPwshStep step:
                        AppendPwshStep(step);
                        break;
                    case AdoCheckoutStep step:
                        AppendCheckoutStep(step);
                        break;
                    case AdoPublishStep step:
                        AppendPublishStep(step);
                        break;
                    case AdoDownloadStep step:
                        AppendDownloadStep(step);
                        break;
                    case AdoTemplateStep step:
                        AppendTemplateStep(step);
                        break;
                    case AdoReviewAppStep step:
                        AppendReviewAppStep(step);
                        break;
                    case AdoGetPackageStep step:
                        AppendGetPackageStep(step);
                        break;
                    case AdoPowershellStep step:
                        AppendPowershellStep(step);
                        break;
                    case AdoDownloadBuildStep step:
                        AppendDownloadBuildStep(step);
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
        }

        private void AppendBashStep(AdoBashStep step)
        {
            AppendScriptLine("bash", step.Bash);
            AppendBaseFields(step);
            if (step.FailOnStderr is not null)
                _builder.AppendLine(2, "failOnStderr: " + step.FailOnStderr);
            if (!string.IsNullOrWhiteSpace(step.WorkingDirectory))
                _builder.AppendLine(2, "workingDirectory: " + step.WorkingDirectory);
        }

        private void AppendScriptStep(AdoScriptStep step)
        {
            AppendScriptLine("script", step.Script);
            AppendBaseFields(step);
            if (step.FailOnStderr is not null)
                _builder.AppendLine(2, "failOnStderr: " + step.FailOnStderr);
            if (!string.IsNullOrWhiteSpace(step.WorkingDirectory))
                _builder.AppendLine(2, "workingDirectory: " + step.WorkingDirectory);

        }

        private void AppendTaskStep(AdoTaskStep step)
        {
            _builder.AppendLine(0, "- task: " + step.Task);
            AppendBaseFields(step);
            if (step.Inputs?.Count > 0)
            {
                _builder.AppendLine(2, "inputs:");
                _builder.AppendKeyValuePairs(2, step.Inputs);
            }
        }

        private void AppendPwshStep(AdoPwshStep step)
        {
            AppendScriptLine("pwsh", step.Pwsh);
            AppendBaseFields(step);
            if (step.FailOnStderr is not null)
                _builder.AppendLine(2, "failOnStderr: " + step.FailOnStderr);
            if (!string.IsNullOrWhiteSpace(step.ErrorActionPreference))
                _builder.AppendLine(2, "errorActionPreference: " + step.ErrorActionPreference);
            if (step.IgnoreLASTEXITCODE is not null)
                _builder.AppendLine(2, "ignoreLASTEXITCODE: " + step.IgnoreLASTEXITCODE);
            if (!string.IsNullOrWhiteSpace(step.WorkingDirectory))
                _builder.AppendLine(2, "workingDirectory: " + step.WorkingDirectory);

        }

        private void AppendCheckoutStep(AdoCheckoutStep step)
        {
            _builder.AppendLine(0, "- checkout: " + step.Checkout);
            if (step.Clean is not null)
                _builder.AppendLine(2, "clean: " + step.Clean);
            if (step.FetchDepth is not null)
                _builder.AppendLine(2, "fetchDepth: " + step.FetchDepth);
            if (!string.IsNullOrWhiteSpace(step.FetchFilter))
                _builder.AppendLine(2, "fetchFilter: " + step.FetchFilter);
            if (step.FetchTags is not null)
                _builder.AppendLine(2, "fetchTags: " + step.FetchTags);
            if (step.Lfs is not null)
                _builder.AppendLine(2, "lfs: " + step.Lfs);
            if (step.PersistCredentials is not null)
                _builder.AppendLine(2, "persistCredentials: " + step.PersistCredentials);
            if (!string.IsNullOrWhiteSpace(step.Submodules))
                _builder.AppendLine(2, "submodules: " + step.Submodules);
            if (!string.IsNullOrWhiteSpace(step.Path))
                _builder.AppendLine(2, "path: " + step.Path);
            if (step.WorkspaceRepo is not null)
                _builder.AppendLine(2, "workspaceRepo: " + step.WorkspaceRepo);
            AppendBaseFields(step);
        }

        private void AppendPublishStep(AdoPublishStep step)
        {
            _builder.AppendLine(0, "- publish: " + step.Publish);
            if (!string.IsNullOrWhiteSpace(step.Artifact))
                _builder.AppendLine(2, "artifact: " + step.Artifact);
            AppendBaseFields(step);
        }

        private void AppendDownloadStep(AdoDownloadStep step)
        {
            _builder.AppendLine(0, "- download: " + step.Download);
            if (!string.IsNullOrWhiteSpace(step.Artifact))
                _builder.AppendLine(2, "artifact: " + step.Artifact);
            if (!string.IsNullOrWhiteSpace(step.Patterns))
                _builder.AppendLine(2, "patterns: " + step.Patterns);
            AppendBaseFields(step);
        }

        private void AppendTemplateStep(AdoTemplateStep step)
        {
            _builder.AppendLine(0, "- template: " + step.Template);
            AppendBaseFields(step);
            _builder.Append(2, step.Parameters);

        }

        private void AppendReviewAppStep(AdoReviewAppStep step)
        {
            _builder.AppendLine(0, "- reviewApp: " + step.ReviewApp);
            AppendBaseFields(step);
        }

        private void AppendGetPackageStep(AdoGetPackageStep step)
        {
            _builder.AppendLine(0, "- getPackage: " + step.GetPackage);
            _builder.AppendLine(2, "path: " + step.Path);
            AppendBaseFields(step);
        }

        private void AppendPowershellStep(AdoPowershellStep step)
        {
            AppendScriptLine("powershell", step.Powershell);
            AppendBaseFields(step);
            if (step.FailOnStderr is not null)
                _builder.AppendLine(2, "failOnStderr: " + step.FailOnStderr);
            if (!string.IsNullOrWhiteSpace(step.ErrorActionPreference))
                _builder.AppendLine(2, "errorActionPreference: " + step.ErrorActionPreference);
            if (step.IgnoreLASTEXITCODE is not null)
                _builder.AppendLine(2, "ignoreLASTEXITCODE: " + step.IgnoreLASTEXITCODE);
            if (!string.IsNullOrWhiteSpace(step.WorkingDirectory))
                _builder.AppendLine(2, "workingDirectory: " + step.WorkingDirectory);


        }

        private void AppendDownloadBuildStep(AdoDownloadBuildStep step)
        {
            _builder.AppendLine(0, "- downloadBuild: " + step.DownloadBuild);
            if (!string.IsNullOrWhiteSpace(step.Artifact))
                _builder.AppendLine(2, "artifact: " + step.Artifact);
            if (!string.IsNullOrWhiteSpace(step.Path))
                _builder.AppendLine(2, "path: " + step.Path);
            if (!string.IsNullOrWhiteSpace(step.Patterns))
                _builder.AppendLine(2, "patterns: " + step.Patterns);
            AppendBaseFields(step);
        }

        public void AppendBaseFields(AdoStepBase step)
        {
            if (!string.IsNullOrEmpty(step.Name))
                _builder.AppendLine(2, "name: " + step.Name);
            if (!string.IsNullOrEmpty(step.DisplayName))
                _builder.AppendLine(2, "displayName: " + step.DisplayName);
            if (step.TimeoutInMinutes is not null)
                _builder.AppendLine(2, "timeoutInMinutes: " + step.TimeoutInMinutes);
            if (step.ContinueOnError is not null)
                _builder.AppendLine(2, "continueOnError: " + step.ContinueOnError);
            if (!string.IsNullOrEmpty(step.Condition))
                _builder.AppendLine(2, "condition: " + step.Condition);
            if (step.RetryCountOnTaskFailure is not null)
                _builder.AppendLine(2, "retryCountOnTaskFailure: " + step.RetryCountOnTaskFailure);
            if (!string.IsNullOrWhiteSpace(step.Target))
                _builder.AppendLine(2, "target: " + step.Target);
            if (step.Env?.Count > 0)
            {
                _builder.AppendLine(2, "env:");
                _builder.AppendKeyValuePairs(2, step.Env);
            }
            if (step.Enabled is not null)
                _builder.AppendLine(2, "enabled: " + step.Enabled);
        }

        private void AppendScriptLine(string label, string script)
        {
            if (script.Contains(Environment.NewLine))
            {
                _builder.AppendLine(0, $"- {label}: |");
                _builder.AppendLine(2, script);
            }
            else
                _builder.AppendLine(0, "- " + label + ": " + script);
        }
    }
}