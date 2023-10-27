# Next Steps after `azd init`

## Table of Contents

1. [Next Steps](#next-steps)
2. [What was added](#what-was-added)
3. [Billing](#billing)
4. [Troubleshooting](#troubleshooting)

## Next Steps

### Provision infrastructure and deploy application code

Run `azd up` to provision your infrastructure and deploy to Azure in one step (or run `azd provision` then `azd deploy` to accomplish the tasks separately). Visit the service endpoints listed to see your application up-and-running!

To troubleshoot any issues, see [troubleshooting](#troubleshooting).

### Configure CI/CD pipeline

1. Create a workflow pipeline file locally. The following starters are available:
   - [Deploy with GitHub Actions](https://github.com/Azure-Samples/azd-starter-bicep/blob/main/.github/workflows/azure-dev.yml)
   - [Deploy with Azure Pipelines](https://github.com/Azure-Samples/azd-starter-bicep/blob/main/.azdo/pipelines/azure-dev.yml)
2. Run `azd pipeline config -e <environment name>` to configure the deployment pipeline to connect securely to Azure. An environment name is specified here to configure the pipeline with a different environment for isolation purposes. Run `azd env list` and `azd env set` to reselect the default environment after this step.

## What was added

Coming Soon!

## Billing

Visit the *Cost Management + Billing* page in Azure Portal to track current spend. For more information about how you're billed, and how you can monitor the costs incurred in your Azure subscriptions, visit [billing overview](https://learn.microsoft.com/en-us/azure/developer/intro/azure-developer-billing).

## Troubleshooting

Q: I visited the service endpoint listed, and I'm seeing a blank or error page.

A: Your service may have failed to start or misconfigured. To investigate further:

1. Click on the resource group link shown to visit Azure Portal.
2. Navigate to the specific Azure Container App resource for the service.
3. Select *Monitoring -> Log stream* under the navigation pane.
4. Observe the log output to identify any errors.
5. If there are no errors, ensure that the ingress target port matches the port that your service listens on:
    1. Under *Settings -> Ingress*, ensure the *Target port* matches the desired port.
    2. After modifying this setting, also update the `targetPort` setting in the .bicep file for the service under `infra/app`.
6. If logs are written to disk, examine the local logs or debug the application by using the *Console* to connect to a shell within the running container.

For additional information about setting up your `azd` project, visit our official [docs](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/make-azd-compatible?pivots=azd-convert).
