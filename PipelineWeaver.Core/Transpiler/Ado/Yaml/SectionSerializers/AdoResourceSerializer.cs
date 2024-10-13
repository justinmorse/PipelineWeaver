using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;


namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoResourceSerializer : IAdoYamlSectionSerializer
    {
        internal AdoYamlBuilder _builder = new AdoYamlBuilder();

        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var resource = section as AdoSectionCollection<IAdoResource> ?? throw new ArgumentException(nameof(section));

            _builder.AppendLine(0, "resources:");
            if (resource.Count > 0)
            {
                AppendPipelineResources(resource, 2);
                AppendBuildResources(resource, 2);
                AppendRepositoryResources(resource, 2);
                AppendContainerResources(resource, 2);
                AppendPackageResources(resource, 2);
            }

            builder.AppendLine(startingIndent, _builder.ToString(), true, true);
        }

        private void AppendPipelineResources(AdoSectionCollection<IAdoResource> resources, int startingIndent)
        {
            var pipelineResources = resources?.Where(r => r is AdoPipelineResource).ToList() ?? new List<IAdoResource>();
            if (!pipelineResources.Any())
                return;

            _builder.AppendLine(startingIndent, "pipelines:");
            pipelineResources.ForEach(r =>
            {
                var pipelineResource = r as AdoPipelineResource;
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Pipeline))
                    _builder.AppendLine(startingIndent, $"- pipeline: {pipelineResource.Pipeline}");
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Connection))
                    _builder.AppendLine(startingIndent + 2, $"connection: {pipelineResource.Connection}");
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Project))
                    _builder.AppendLine(startingIndent + 2, $"project: {pipelineResource.Project}");
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Source))
                    _builder.AppendLine(startingIndent + 2, $"source: {pipelineResource.Source}");
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Version))
                    _builder.AppendLine(startingIndent + 2, $"version: {pipelineResource.Version}");
                if (!string.IsNullOrWhiteSpace(pipelineResource?.Branch))
                    _builder.AppendLine(startingIndent + 2, $"branch: {pipelineResource.Branch}");
                if (pipelineResource?.Tags?.Count > 0)
                    _builder.AppendLine(startingIndent + 2, $"tags: {pipelineResource.Tags?.Join(" ")}");
            });
        }

        private void AppendBuildResources(AdoSectionCollection<IAdoResource> resources, int startingIndent)
        {
            var buildResources = resources?.Where(r => r is AdoBuildResource).ToList() ?? new List<IAdoResource>();
            if (buildResources.Count == 0)
                return;

            _builder.AppendLine(startingIndent, "builds:");
            buildResources.ForEach(r =>
            {
                var buildResource = r as AdoBuildResource;
                if (!string.IsNullOrWhiteSpace(buildResource?.Build))
                    _builder.AppendLine(startingIndent, $"- build: {buildResource.Build}");
                if (!string.IsNullOrWhiteSpace(buildResource?.Type))
                    _builder.AppendLine(startingIndent + 2, $"project: {buildResource.Type}");
                if (!string.IsNullOrWhiteSpace(buildResource?.Connection))
                    _builder.AppendLine(startingIndent + 2, $"connection: {buildResource.Connection}");
                if (!string.IsNullOrWhiteSpace(buildResource?.Source))
                    _builder.AppendLine(startingIndent + 2, $"source: {buildResource.Source}");
                if (buildResource?.Trigger != null)
                    _builder.AppendLine(startingIndent + 2, $"trigger: {buildResource.Trigger.Value}");
            });
        }
        private void AppendRepositoryResources(AdoSectionCollection<IAdoResource> resources, int startingIndent)
        {
            var repoResources = resources?.Where(r => r is AdoRepositoryResource).ToList() ?? new List<IAdoResource>();
            if (repoResources.Count == 0)
                return;

            _builder.AppendLine(startingIndent, "repositories:");
            repoResources.ForEach(r =>
            {
                var brepoResource = r as AdoRepositoryResource;
                if (!string.IsNullOrWhiteSpace(brepoResource?.Repository))
                    _builder.AppendLine(startingIndent, $"- repository: {brepoResource.Repository}");
                if (!string.IsNullOrWhiteSpace(brepoResource?.Project))
                    _builder.AppendLine(startingIndent + 2, $"project: {brepoResource.Project}");
                if (!string.IsNullOrWhiteSpace(brepoResource?.Type))
                    _builder.AppendLine(startingIndent + 2, $"type: {brepoResource.Type}");
                if (!string.IsNullOrWhiteSpace(brepoResource?.Connection))
                    _builder.AppendLine(startingIndent + 2, $"connection: {brepoResource.Connection}");
                if (!string.IsNullOrWhiteSpace(brepoResource?.Source))
                    _builder.AppendLine(startingIndent + 2, $"source: {brepoResource.Source}");
                if (string.IsNullOrWhiteSpace(brepoResource?.Endpoint))
                    _builder.AppendLine(startingIndent + 2, $"endpoint: {brepoResource?.Endpoint}");
            });
        }

        private void AppendContainerResources(AdoSectionCollection<IAdoResource> resources, int startingIndent)
        {
            var containerResources = resources.Where(r => r is AdoContainerResource).ToList();
            if (containerResources.Count == 0)
                return;
            _builder.AppendLine(startingIndent, "containers:");
            containerResources.ForEach(r =>
            {
                var containerResource = r as AdoContainerResource;
                if (!string.IsNullOrWhiteSpace(containerResource?.Container))
                    _builder.AppendLine(startingIndent, $"- container: {containerResource.Container}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Connection))
                    _builder.AppendLine(startingIndent + 2, $"connection: {containerResource.Connection}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Image))
                    _builder.AppendLine(startingIndent + 2, $"image: {containerResource.Image}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Options))
                    _builder.AppendLine(startingIndent + 2, $"options: {containerResource.Options}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Env))
                    _builder.AppendLine(startingIndent + 2, $"env: {containerResource.Env}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Ports))
                    _builder.AppendLine(startingIndent + 2, $"ports: {containerResource.Ports}");
                if (!string.IsNullOrWhiteSpace(containerResource?.Volumes))
                    _builder.AppendLine(startingIndent + 2, $"volumes: {containerResource.Volumes}");
            });
        }

        private void AppendPackageResources(AdoSectionCollection<IAdoResource> resources, int startingIndent)
        {
            var packageResources = resources?.Where(r => r is AdoPackageResource).ToList() ?? new List<IAdoResource>();
            if (packageResources.Count == 0)
                return;

            _builder.AppendLine(startingIndent, "packages:");
            packageResources.ForEach(r =>
            {
                var packageResource = r as AdoPackageResource;
                if (packageResource != null)
                {

                    if (!string.IsNullOrWhiteSpace(packageResource.Package))
                        _builder.AppendLine(startingIndent, $"- package: {packageResource.Package}");
                    if (!string.IsNullOrWhiteSpace(packageResource.Type))
                        _builder.AppendLine(startingIndent + 2, $"type: {packageResource.Type}");
                    if (!string.IsNullOrWhiteSpace(packageResource.Connection))
                        _builder.AppendLine(startingIndent + 2, $"connection: {packageResource.Connection}");
                    if (!string.IsNullOrWhiteSpace(packageResource.Version))
                        _builder.AppendLine(startingIndent + 2, $"verion: {packageResource.Version}");
                    if (string.IsNullOrWhiteSpace(packageResource.Endpoint))
                        _builder.AppendLine(startingIndent + 2, $"tag: {packageResource.Tag}");
                }
            });
        }

    }
}