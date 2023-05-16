# Dynamic DNS Update

An application which updates the DNS records in your DNS providers when your external/internet-facing IP address changes.

Update checks can occur on a frequency specified in configuration.

## Supported DNS providers

* DigitalOcean
* GoDaddy

## Supported DNS record types

* Address (**A**)

More record types will be added at a later date.

## Getting started

1. Pull the DDNSUpdate package from the [DDNSUpdate Repository at GitHub](https://github.com/users/TheDanielDoyle/packages/container/package/ddnsupdate).

    ``docker pull ghcr.io/thedanieldoyle/ddnsupdate:latest``

2. Set up configuration using either config file, environment variables or Docker secrets.

3. Run the application using either Docker CLI or Docker Compose.

### Docker CLI

* The following is an example using config.json file configuration and *docker run*.

    ``docker run -d --name ddnsupdate -v /apps/ddnsupdate/config.json:/app/config.json ghcr.io/thedanieldoyle/ddnsupdate:latest``

### Docker-Compose

* The following is an example using *docker-compose*.

    ``docker-compose up -d``

* See the [Docker sample](https://github.com/TheDanielDoyle/DDNSUpdate/tree/master/samples/) folder for an example of a docker-compose.yml file.

## Configuration

DDNSUpdate supports three configuration types (all three may be used at the same time):

* Configuration file
* Environment variables
* Docker secrets

The easiest to set up and use is the configuration file. It is possible however to keep all non-sensitve configuration in a configuration file and use Docker secrets or environment variables to configure senstive values.

### File configuration

* The config.json or config.yml/yaml must be bound using a bind mount if you are using file configuration.
* See the [Configuration sample](https://github.com/TheDanielDoyle/DDNSUpdate/tree/master/samples/Configuration) folder for examples of config.json and config.yml/yaml file configuration.
* The config.json or config.yml/yaml must be bound in the container to either:
  * /app/config.json
  * /app/config.yml
  * /app/config.yaml

### Environment Variable configuration

* The following example configures two External Address providers.

    ``ExternalAddressProviders__0__Uri=https://bot.whatismyipaddress.com/``
    ``ExternalAddressProviders__1__Uri=https://ipv4bot.whatismyipaddress.com/``

* See [ASP.NET Core Environment configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#environment-variables) for instructions on how to structure the Environment Variable names.

### Docker Secrets configuration

* As with the environment variables, name your Docker Secrets the same way.

    ``printf "https://bot.whatismyipaddress.com/" | docker secret create ExternalAddressProviders__0__Uri -``

    ``printf "https://ipv4bot.whatismyipaddress.com/" | docker secret create ExternalAddressProviders__1__Uri -``

* See [ASP.NET Core Key-per-file configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#key-per-file-configuration-provider) for instructions on how to structure the Docker Secret names.

## Build docker image

DDNSUpdate supports multi-platform builds with [buildx](https://docs.docker.com/engine/reference/commandline/buildx/), post dotnet SDK verison **7.0.300**.

### Example build for Linux AM64 and ARM64

_docker_registry_address_ could be `dandoyle.com` for example

```shell
export DOCKERFILE=DDNSUpdate/Dockerfile;
export REGISTRY=docker_registry_address;
export SERVICE=ddnsupdate;
export VERSION=1.0.0;
docker buildx build --platform linux/amd64,linux/arm64 --tag "${REGISTRY}/$SERVICE:$VERSION" --build-arg VERSION --file $DOCKERFILE --push .
```
To support multi-platform builds, changes were required to the dotnet SDK from Microsoft, and also use of some automatic ARGs from Docker. Namely **BUILDPLATFORM** and **TARGETARCH**. See [Docker documentation](https://docs.docker.com/engine/reference/builder/#automatic-platform-args-in-the-global-scope). 

Also, the build command above will attempt to push to your docker registry using **--push**, therefore be sure to log in first.

If you just want to build an image for local development, change **--push** to **--load**.

#### Fun fact

You may notice on the **docker buildx build** command, the **--build-arg VERSION** is not using the variable with **$** symbol, as set above. This is by design. The [Docker documentation](https://docs.docker.com/engine/reference/commandline/build/#build-arg) explains.

> You may also use the --build-arg flag without a value, in which case the value from the local environment will be propagated into the Docker container

As the variable in the shell scope and inside the Dockerfile are the same name, you don't need to specify it in **VERSION=$VERSION** syntax when building. Neat!

## Authors

* **Daniel P. Doyle** - [TheDanielDoyle](https://github.com/TheDanielDoyle/)
* **Adam Parker** - [ParkerAdam](https://github.com/parkeradam/)
* **Michael Willis** - [Mickey-Willis](https://github.com/mickey-willis/)

See also the list of [contributors](https://github.com/TheDanielDoyle/DDNSUpdate/contributors) who participated in this project.

## Acknowledgements

We would like to thank the following open source projects.

| Project                    | Repository                                               | License                                                                                 |
| -------------------------- | -------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| Ardalis.SmartEnum          | https://github.com/ardalis/SmartEnum                     | [LICENSE](https://github.com/ardalis/SmartEnum/blob/master/LICENSE)                     |
| AutoMapper                 | https://github.com/AutoMapper/AutoMapper                 | [LICENSE](https://github.com/AutoMapper/AutoMapper/blob/master/LICENSE.txt)             |
| FakeItEasy                 | https://github.com/FakeItEasy/FakeItEasy                 | [LICENSE](https://github.com/FakeItEasy/FakeItEasy/blob/master/License.txt)             |
| FluentResults              | https://github.com/altmann/FluentResults                 | [LICENSE](https://github.com/altmann/FluentResults/blob/master/LICENSE)                 |
| FluentValidation           | https://github.com/FluentValidation/FluentValidation     | [LICENSE](https://github.com/FluentValidation/FluentValidation/blob/master/License.txt) |
| Flurl                      | https://github.com/tmenier/Flurl                         | [LICENSE](https://github.com/tmenier/Flurl/blob/dev/LICENSE)                            |
| NetEscapades.Configuration | https://github.com/andrewlock/NetEscapades.Configuration | [LICENSE](https://github.com/andrewlock/NetEscapades.Configuration/blob/master/LICENSE) |
| Newtonsoft.Json            | https://github.com/JamesNK/Newtonsoft.Json               | [LICENSE](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)            |
| Serilog                    | https://github.com/serilog/serilog                       | [LICENSE](https://github.com/serilog/serilog/blob/dev/LICENSE)                          |
| xunit                      | https://github.com/xunit/xunit                           | [LICENSE](https://github.com/xunit/xunit/blob/main/LICENSE)                             |
| YamlDotNet                 | https://github.com/aaubry/YamlDotNet                     | [LICENSE](https://github.com/aaubry/YamlDotNet/blob/master/LICENSE.txt)                 |

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.
