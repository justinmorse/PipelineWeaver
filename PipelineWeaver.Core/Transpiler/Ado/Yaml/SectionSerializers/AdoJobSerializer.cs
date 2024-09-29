using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoJobSerializer : IAdoYamlSectionSerializer
    {
        internal AdoYamlBuilder _builder = new AdoYamlBuilder();

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var jobs = section as AdoSectionCollection<AdoJobBase> ?? throw new ArgumentException(nameof(section));
            if (builder is not null)
                _builder = builder;

            if (jobs.Count > 0)
            {
                AppendJobs(jobs, startingIndent);
            }
        }

        private void AppendJobs(AdoSectionCollection<AdoJobBase> job, int startingIndent)
        {
            if (job.Count > 0)
            {
                _builder.AppendLine(startingIndent, "jobs:");
                job.ToList().ForEach(j => AppendJob(j, startingIndent + 2));
            }
        }

        private void AppendJob(AdoJobBase j, int startingIndent)
        {
            switch (j)
            {
                case AdoJob job:
                    AppendBasicJob(job, startingIndent);
                    break;
                case AdoDeploymentJob job:
                    AppendDeploymentJob(job, startingIndent);
                    break;
                case AdoTemplateJob job:
                    AppendJobTemplate(job, startingIndent);
                    break;
                default:
                    throw new ArgumentException(nameof(j));
            }
        }

        private void AppendJobTemplate(AdoTemplateJob job, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 2, "- template: " + job.Template);
            if (job.Parameters?.Count > 0)
            {
                _builder.AppendLine(startingIndent + 4, "parameters:");
                _builder.Append(4 + startingIndent, job.Parameters);
            }
        }

        private void AppendDeploymentJob(AdoDeploymentJob job, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 2, "- deployment: " + job.Deployment);
            AppendBaseFields(job, startingIndent);
            _builder.Append(startingIndent + 4, job.Strategy);
        }

        private void AppendBasicJob(AdoJob job, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 2, "- job: " + job.Job);
            AppendBaseFields(job, startingIndent);
            if (job.Strategy is not null)
            {
                _builder.AppendLine(startingIndent + 4, "strategy:");
                if (job.Strategy.Matrix is not null)
                    _builder.AppendLine(startingIndent + 6, "matrix:" + job.Strategy.Matrix.ToJson());
                if (job.Strategy.MaxParallel is not null)
                    _builder.AppendLine(startingIndent + 6, "maxParallel: " + job.Strategy.MaxParallel);
            }

            if (job.Steps?.Count() > 0)
            {
                _builder.AppendLine(startingIndent + 4, "steps:");
                _builder.Append(startingIndent + 6, job.Steps);
            }
        }

        private void AppendBaseFields(AdoJobBase job, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 4, "displayName: " + job.DisplayName);
            if (job.DependsOn?.Count > 0)
                _builder.AppendList("dependsOn", 4 + startingIndent, job.DependsOn);
            if (string.IsNullOrEmpty(job.Condition))
                _builder.AppendLine(startingIndent + 4, "condition: " + job.Condition);
            if (job.ContinueOnError ?? false)
                _builder.AppendLine(startingIndent + 4, "continueOnError: true");
            if (job.TimeoutInMinutes is not null)
                _builder.AppendLine(startingIndent + 4, "timeoutInMinutes: " + job.TimeoutInMinutes);
            if (job.CancelTimeoutInMinutes is not null)
                _builder.AppendLine(startingIndent + 4, "cancelTimeoutInMinutes: " + job.CancelTimeoutInMinutes);
            if (job.Variables is not null)
            {
                _builder.AppendLine(startingIndent + 4, "variables: ");
                _builder.Append(startingIndent + 6, job.Variables);
            }
            if (job.Pool is not null)
                _builder.AppendLine(startingIndent + 4, "pool: " + job.Pool);
            if (job.Container is not null)
            {
                _builder.AppendLine(startingIndent + 4, "container:");
                _builder.AppendLine(startingIndent + 6, "image: " + job.Container.Image);
                if (!string.IsNullOrWhiteSpace(job.Container.Endpoint))
                    _builder.AppendLine(startingIndent + 6, "endpoint: " + job.Container.Endpoint);
                if (job.Container.Env is not null)
                    _builder.AppendKeyValuePairs("env", startingIndent + 6, job.Container.Env);
                if (job.Container.MapDockerSocket is not null)
                    _builder.AppendLine(startingIndent + 6, "mapDockerSocket: " + job.Container.MapDockerSocket);
                if (!string.IsNullOrWhiteSpace(job.Container.Options))
                    _builder.AppendLine(startingIndent + 6, "options: " + job.Container.Options);
                if (job.Container.Ports?.Count > 0)
                    _builder.AppendList("ports", startingIndent + 6, job.Container.Ports);
                if (job.Container.Volumes?.Count > 0)
                    _builder.AppendList("volumes", startingIndent + 6, job.Container.Volumes);
                if (job.Container.MountReadOnly is not null)
                {
                    _builder.AppendLine(startingIndent + 6, "mountReadOnly:");
                    if (job.Container.MountReadOnly.Work is not null)
                        _builder.AppendLine(startingIndent + 8, "work: " + job.Container.MountReadOnly.Work);
                    if (job.Container.MountReadOnly.Externals is not null)
                        _builder.AppendLine(startingIndent + 8, "externals: " + job.Container.MountReadOnly.Externals);
                    if (job.Container.MountReadOnly.Tools is not null)
                        _builder.AppendLine(startingIndent + 8, "tools: " + job.Container.MountReadOnly.Tools);
                    if (job.Container.MountReadOnly.Tasks is not null)
                        _builder.AppendLine(startingIndent + 8, "tasks: " + job.Container.MountReadOnly.Tasks);
                }
            }
            if (job.Services is not null)
            {
                _builder.AppendKeyValuePairs("services", startingIndent + 4, job.Services);
            }
            if (job.Workspace is not null)
            {
                _builder.AppendLine(startingIndent + 4, "workspace:");
                _builder.AppendLine(startingIndent + 6, "clean: " + job.Workspace.Clean);
            }
            if (job.Uses is not null)
            {
                _builder.AppendLine(startingIndent + 4, "uses:");
                if (job.Uses.Repositories?.Count > 0)
                    _builder.AppendList("repositories", startingIndent + 6, job.Uses.Repositories);
                if (job.Uses.Pools?.Count > 0)
                    _builder.AppendList("pools", startingIndent + 6, job.Uses.Pools);
            }
            if (!string.IsNullOrWhiteSpace(job.TemplateContext))
                _builder.AppendLine(startingIndent + 4, "templateContext: " + job.TemplateContext);

        }
    }
}