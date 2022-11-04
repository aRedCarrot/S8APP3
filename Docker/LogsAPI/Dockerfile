#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 22

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LogsAPI/LogsAPI.csproj", "LogsAPI/"]
RUN dotnet restore "LogsAPI/LogsAPI.csproj"
COPY . .
WORKDIR "/src/LogsAPI"
RUN dotnet build "LogsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# SSH
COPY ["LogsAPI/SettingTripWire.txt", "."]
COPY ["LogsAPI/tripwireExec.sh", "."]
RUN apt-get -y update
RUN apt-get -y install openssh-server net-tools sudo
RUN chmod -R 777 /app
RUN useradd -m admin && echo "admin:admin" | chpasswd && adduser admin sudo
RUN usermod -aG sudo admin
RUN mkdir /var/run/sshd
# authorize SSH connection with root account
RUN sed 's@session\s*required\s*pam_loginuid.so@session optional pam_loginuid.so@g' -i /etc/pam.d/sshd
# tripwire
#RUN no| apt-get -y install tripwire
#RUN cat SettingTripWire.txt >> /etc/tripwire/twpol.txt
#RUN twadmin --create-polfile -s -Q "" -S /etc/tripwire/site.key /etc/tripwire/twpol.txt && tripwire --init -P ""
ENTRYPOINT service ssh restart && DEBIAN_FRONTEND=noninteractive apt-get -y install tripwire && bash /app/tripwireExec.sh && dotnet LogsAPI.dll
