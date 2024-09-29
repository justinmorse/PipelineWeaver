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

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var stepSection = section as AdoSectionCollection<AdoStepBase> ?? throw new ArgumentException(nameof(section));
            if (builder is not null)
                _builder = builder;

            _builder.AppendLine(startingIndent, "steps:");
            AppendSteps(stepSection, startingIndent + 2);
        }


        public void AppendSteps(AdoSectionCollection<AdoStepBase> steps, int startingIndent)
        {
            foreach (var s in steps)
            {
                switch (s)
                {
                    case AdoScriptStep step:
                        AppendScriptStep(step, startingIndent);
                        break;
                    case AdoTaskStep step:
                        AppendTaskStep(step, startingIndent);
                        break;
                    case AdoBashStep step:
                        AppendBashStep(step, startingIndent);
                        break;
                    case AdoPwshStep step:
                        AppendPwshStep(step, startingIndent);
                        break;
                    case AdoCheckoutStep step:
                        AppendCheckoutStep(step, startingIndent);
                        break;
                    case AdoPublishStep step:
                        AppendPublishStep(step, startingIndent);
                        break;
                    case AdoDownloadStep step:
                        AppendDownloadStep(step, startingIndent);
                        break;
                    case AdoTemplateStep step:
                        AppendTemplateStep(step, startingIndent);
                        break;
                    case AdoReviewAppStep step:
                        AppendReviewAppStep(step, startingIndent);
                        break;
                    case AdoGetPackageStep step:
                        AppendGetPackageStep(step, startingIndent);
                        break;
                    case AdoPowershellStep step:
                        AppendPowershellStep(step, startingIndent);
                        break;
                    case AdoDownloadBuildStep step:
                        AppendDownloadBuildStep(step, startingIndent);
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
        }

        private void AppendBashStep(AdoBashStep step, int startingIndent)
        {
            AppendScriptLine("bash", step.Bash, startingIndent);
            _builder.AppendLine(startingIndent + 2, "failOnStderr: " + step.FailOnStderr);
            _builder.AppendLine(startingIndent + 2, "workingDirectory: " + step.WorkingDirectory);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendScriptStep(AdoScriptStep step, int startingIndent)
        {
            AppendScriptLine("script", step.Script, startingIndent);
            _builder.AppendLine(startingIndent + 2, "failOnStderr: " + step.FailOnStderr);
            _builder.AppendLine(startingIndent + 2, "workingDirectory: " + step.WorkingDirectory);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendTaskStep(AdoTaskStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- task: " + step.Task);
            _builder.AppendKeyValuePairs("inputs", startingIndent + 2, step.Inputs);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendPwshStep(AdoPwshStep step, int startingIndent)
        {
            AppendScriptLine("pwsh", step.Pwsh, startingIndent);
            _builder.AppendLine(startingIndent + 2, "failOnStderr: " + step.FailOnStderr);
            _builder.AppendLine(startingIndent + 2, "errorActionPreference: " + step.ErrorActionPreference);
            _builder.AppendLine(startingIndent + 2, "ignoreLASTEXITCODE: " + step.IgnoreLASTEXITCODE);
            _builder.AppendLine(startingIndent + 2, "workingDirectory: " + step.WorkingDirectory);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendCheckoutStep(AdoCheckoutStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- checkout: " + step.Checkout);
            _builder.AppendLine(startingIndent + 2, "clean: " + step.Clean);
            _builder.AppendLine(startingIndent + 2, "fetchDepth: " + step.FetchDepth);
            _builder.AppendLine(startingIndent + 2, "fetchFilter: " + step.FetchFilter);
            _builder.AppendLine(startingIndent + 2, "fetchTags: " + step.FetchTags);
            _builder.AppendLine(startingIndent + 2, "lfs: " + step.Lfs);
            _builder.AppendLine(startingIndent + 2, "persistCredentials: " + step.PersistCredentials);
            _builder.AppendLine(startingIndent + 2, "submodules: " + step.Submodules);
            _builder.AppendLine(startingIndent + 2, "path: " + step.Path);
            _builder.AppendLine(startingIndent + 2, "workspaceRepo: " + step.WorkspaceRepo);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendPublishStep(AdoPublishStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- publish: " + step.Publish);
            _builder.AppendLine(startingIndent + 2, "artifact: " + step.Artifact);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendDownloadStep(AdoDownloadStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- download: " + step.Download);
            _builder.AppendLine(startingIndent + 2, "artifact: " + step.Artifact);
            _builder.AppendLine(startingIndent + 2, "patterns: " + step.Patterns);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendTemplateStep(AdoTemplateStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- template: " + step.Template);
            _builder.Append(startingIndent + 2, step.Parameters);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendReviewAppStep(AdoReviewAppStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- reviewApp: " + step.ReviewApp);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendGetPackageStep(AdoGetPackageStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- getPackage: " + step.GetPackage);
            _builder.AppendLine(startingIndent + 2, "path: " + step.Path);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendPowershellStep(AdoPowershellStep step, int startingIndent)
        {
            AppendScriptLine("powershell", step.Powershell, startingIndent);
            _builder.AppendLine(startingIndent + 2, "failOnStderr: " + step.FailOnStderr);
            _builder.AppendLine(startingIndent + 2, "errorActionPreference: " + step.ErrorActionPreference);
            _builder.AppendLine(startingIndent + 2, "ignoreLASTEXITCODE: " + step.IgnoreLASTEXITCODE);
            _builder.AppendLine(startingIndent + 2, "workingDirectory: " + step.WorkingDirectory);
            AppendBaseFields(step, startingIndent + 2);
        }

        private void AppendDownloadBuildStep(AdoDownloadBuildStep step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "- downloadBuild: " + step.DownloadBuild);
            _builder.AppendLine(startingIndent + 2, "artifact: " + step.Artifact);
            _builder.AppendLine(startingIndent + 2, "path: " + step.Path);
            _builder.AppendLine(startingIndent + 2, "patterns: " + step.Patterns);
            AppendBaseFields(step, startingIndent + 2);
        }

        public void AppendBaseFields(AdoStepBase step, int startingIndent)
        {
            _builder.AppendLine(startingIndent, "name: " + step.Name);
            _builder.AppendLine(startingIndent, "displayName: " + step.DisplayName);
            _builder.AppendLine(startingIndent, "timeoutInMinutes: " + step.TimeoutInMinutes);
            _builder.AppendLine(startingIndent, "continueOnError: " + step.ContinueOnError);
            _builder.AppendLine(startingIndent, "condition: " + step.Condition);
            _builder.AppendLine(startingIndent, "retryCountOnTaskFailure: " + step.RetryCountOnTaskFailure);
            _builder.AppendLine(startingIndent, "target: " + step.Target);
            _builder.AppendKeyValuePairs("env", startingIndent, step.Env);
            _builder.AppendLine(startingIndent, "enabled: " + step.Enabled);
        }

        private void AppendScriptLine(string label, string script, int startingIndent)
        {
            if (script.Contains(Environment.NewLine))
            {
                _builder.AppendLine(startingIndent, $"- {label}: |");
                _builder.AppendLine(startingIndent + 2, script);
            }
            else
                _builder.AppendLine(startingIndent, "- " + label + ": " + script);
        }
    }
}