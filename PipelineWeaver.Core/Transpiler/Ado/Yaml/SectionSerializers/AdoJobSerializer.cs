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

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent, bool includeHeader = true)
        {
            var jobs = section as AdoSectionCollection<AdoJobBase> ?? throw new ArgumentException(nameof(section));

            if (jobs.Count > 0)
            {
                AppendJobs(jobs);
                builder.AppendLine(startingIndent, _builder.ToString(), true, true);
            }

        }

        private void AppendJobs(AdoSectionCollection<AdoJobBase> job)
        {
            if (job.Count > 0)
            {
                _builder.AppendLine(0, "jobs:");
                job.ToList().ForEach(j => AppendJob(j));
            }
        }

        private void AppendJob(AdoJobBase j)
        {
            switch (j)
            {
                case AdoJob job:
                    AppendBasicJob(job);
                    break;
                case AdoDeploymentJob job:
                    AppendDeploymentJob(job);
                    break;
                case AdoTemplateJob job:
                    AppendJobTemplate(job);
                    break;
                default:
                    throw new ArgumentException(nameof(j));
            }
        }

        private void AppendJobTemplate(AdoTemplateJob job)
        {
            _builder.AppendLine(0, "- template: " + job.Template);
            if (job.Parameters?.Count > 0)
            {
                _builder.Append(2, job.Parameters);
            }
        }

        private void AppendDeploymentJob(AdoDeploymentJob job)
        {
            _builder.AppendLine(0, "- deployment: " + job.Deployment);
            AppendBaseFields(job);
            if (job.Strategy is not null)
                _builder.Append(4, new AdoSectionCollection<AdoDeploymentStrategyBase>() { job.Strategy });
        }

        private void AppendBasicJob(AdoJob job)
        {
            _builder.AppendLine(0, "- job: " + job.Job);
            AppendBaseFields(job);
            if (job.Strategy is not null)
            {
                _builder.AppendLine(4, "strategy:");
                if (job.Strategy.Matrix is not null)
                    _builder.AppendLine(6, "matrix:" + job.Strategy.Matrix.ToJson());
                if (job.Strategy.MaxParallel is not null)
                    _builder.AppendLine(6, "maxParallel: " + job.Strategy.MaxParallel);
            }

            if (job.Steps?.Count > 0)
            {
                _builder.Append(2, job.Steps);
            }
        }

        private void AppendBaseFields(AdoJobBase job)
        {
            _builder.AppendLine(2, "displayName: " + job.DisplayName);
            if (job.DependsOn?.Count > 0)
                _builder.AppendArray("dependsOn", 2, job.DependsOn.ToArray());
            if (string.IsNullOrEmpty(job.Condition))
                _builder.AppendLine(2, "condition: " + job.Condition);
            if (job.ContinueOnError ?? false)
                _builder.AppendLine(2, "continueOnError: true");
            if (job.TimeoutInMinutes is not null)
                _builder.AppendLine(2, "timeoutInMinutes: " + job.TimeoutInMinutes);
            if (job.CancelTimeoutInMinutes is not null)
                _builder.AppendLine(2, "cancelTimeoutInMinutes: " + job.CancelTimeoutInMinutes);
            if (job.Variables is not null)
            {
                _builder.AppendLine(2, "variables: ");
                _builder.Append(4, job.Variables);
            }
            if (job.Pool is not null)
                _builder.AppendLine(2, "pool: " + job.Pool);
            if (job.Container is not null)
            {
                _builder.AppendLine(2, "container:");
                _builder.AppendLine(4, "image: " + job.Container.Image);
                if (!string.IsNullOrWhiteSpace(job.Container.Endpoint))
                    _builder.AppendLine(4, "endpoint: " + job.Container.Endpoint);
                if (job.Container.Env is not null)
                {
                    _builder.AppendLine(4, "env:");
                    _builder.AppendKeyValuePairs(4, job.Container.Env);
                }
                if (job.Container.MapDockerSocket is not null)
                    _builder.AppendLine(4, "mapDockerSocket: " + job.Container.MapDockerSocket);
                if (!string.IsNullOrWhiteSpace(job.Container.Options))
                    _builder.AppendLine(4, "options: " + job.Container.Options);
                if (job.Container.Ports?.Count > 0)
                    _builder.AppendArray("ports", 4, job.Container.Ports.ToArray());
                if (job.Container.Volumes?.Count > 0)
                    _builder.AppendArray("volumes", 4, job.Container.Volumes.ToArray());
                if (job.Container.MountReadOnly is not null)
                {
                    _builder.AppendLine(4, "mountReadOnly:");
                    if (job.Container.MountReadOnly.Work is not null)
                        _builder.AppendLine(6, "work: " + job.Container.MountReadOnly.Work);
                    if (job.Container.MountReadOnly.Externals is not null)
                        _builder.AppendLine(6, "externals: " + job.Container.MountReadOnly.Externals);
                    if (job.Container.MountReadOnly.Tools is not null)
                        _builder.AppendLine(6, "tools: " + job.Container.MountReadOnly.Tools);
                    if (job.Container.MountReadOnly.Tasks is not null)
                        _builder.AppendLine(6, "tasks: " + job.Container.MountReadOnly.Tasks);
                }
            }
            if (job.Services is not null)
            {
                _builder.AppendLine(0, "services:");
                _builder.AppendKeyValuePairs(0, job.Services);
            }
            if (job.Workspace is not null)
            {
                _builder.AppendLine(0, "workspace:");
                _builder.AppendLine(2, "clean: " + job.Workspace.Clean);
            }
            if (job.Uses is not null)
            {
                _builder.AppendLine(0, "uses:");
                if (job.Uses.Repositories?.Count > 0)
                    _builder.AppendArray("repositories", 2, job.Uses.Repositories.ToArray());
                if (job.Uses.Pools?.Count > 0)
                    _builder.AppendArray("pools", 2, job.Uses.Pools.ToArray());
            }
            if (!string.IsNullOrWhiteSpace(job.TemplateContext))
                _builder.AppendLine(0, "templateContext: " + job.TemplateContext);

        }
    }
}