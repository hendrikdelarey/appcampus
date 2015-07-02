<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AppCampus.Cloud" generation="1" functional="0" release="0" Id="1d716ad7-c18d-4837-ac80-2fcb40f67e75" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AppCampus.CloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/LB:AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
        <inPort name="AppCampus.PortalApi:PortalHttpsEndpoint" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/LB:AppCampus.PortalApi:PortalHttpsEndpoint" />
          </inToChannel>
        </inPort>
        <inPort name="AppCampus.PortalApi:SignboardHttpsEndpoint" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/LB:AppCampus.PortalApi:SignboardHttpsEndpoint" />
          </inToChannel>
        </inPort>
        <inPort name="AppCampus.PortalApi:WebsiteEndpoint" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/LB:AppCampus.PortalApi:WebsiteEndpoint" />
          </inToChannel>
        </inPort>
        <inPort name="AppCampus.PortalApi:WebsiteHttpsEndpoint" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/LB:AppCampus.PortalApi:WebsiteHttpsEndpoint" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AppCampus.PortalApi:apiurl" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:apiurl" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:AppCampusContext" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:AppCampusContext" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:DefaultStorageAccount" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:DefaultStorageAccount" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:DocumentDbAuthKey" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:DocumentDbAuthKey" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:DocumentDbCollection" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:DocumentDbCollection" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:DocumentDbDatabase" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:DocumentDbDatabase" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:DocumentDbEndPoint" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:DocumentDbEndPoint" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:LoggingStorageAccount" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:LoggingStorageAccount" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="AppCampus.PortalApiInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapAppCampus.PortalApiInstances" />
          </maps>
        </aCS>
        <aCS name="Certificate|AppCampus.PortalApi:HttpsCertificate" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapCertificate|AppCampus.PortalApi:HttpsCertificate" />
          </maps>
        </aCS>
        <aCS name="Certificate|AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/MapCertificate|AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:AppCampus.PortalApi:PortalHttpsEndpoint">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/PortalHttpsEndpoint" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:AppCampus.PortalApi:SignboardHttpsEndpoint">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/SignboardHttpsEndpoint" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:AppCampus.PortalApi:WebsiteEndpoint">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/WebsiteEndpoint" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:AppCampus.PortalApi:WebsiteHttpsEndpoint">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/WebsiteHttpsEndpoint" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapAppCampus.PortalApi:apiurl" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/apiurl" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:AppCampusContext" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/AppCampusContext" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:DefaultStorageAccount" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/DefaultStorageAccount" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:DocumentDbAuthKey" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/DocumentDbAuthKey" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:DocumentDbCollection" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/DocumentDbCollection" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:DocumentDbDatabase" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/DocumentDbDatabase" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:DocumentDbEndPoint" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/DocumentDbEndPoint" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:LoggingStorageAccount" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/LoggingStorageAccount" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapAppCampus.PortalApiInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApiInstances" />
          </setting>
        </map>
        <map name="MapCertificate|AppCampus.PortalApi:HttpsCertificate" kind="Identity">
          <certificate>
            <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/HttpsCertificate" />
          </certificate>
        </map>
        <map name="MapCertificate|AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AppCampus.PortalApi" generation="1" functional="0" release="0" software="C:\Workspace\AppCampus\Trunk\AppCampus.Cloud\csx\Release\roles\AppCampus.PortalApi" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="PortalHttpsEndpoint" protocol="https" portRanges="4044">
                <certificate>
                  <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/HttpsCertificate" />
                </certificate>
              </inPort>
              <inPort name="SignboardHttpsEndpoint" protocol="https" portRanges="4049">
                <certificate>
                  <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/HttpsCertificate" />
                </certificate>
              </inPort>
              <inPort name="WebsiteEndpoint" protocol="http" portRanges="80" />
              <inPort name="WebsiteHttpsEndpoint" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/HttpsCertificate" />
                </certificate>
              </inPort>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/SW:AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="apiurl" defaultValue="" />
              <aCS name="AppCampusContext" defaultValue="" />
              <aCS name="DefaultStorageAccount" defaultValue="" />
              <aCS name="DocumentDbAuthKey" defaultValue="" />
              <aCS name="DocumentDbCollection" defaultValue="" />
              <aCS name="DocumentDbDatabase" defaultValue="" />
              <aCS name="DocumentDbEndPoint" defaultValue="" />
              <aCS name="LoggingStorageAccount" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AppCampus.PortalApi&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AppCampus.PortalApi&quot;&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;e name=&quot;PortalHttpsEndpoint&quot; /&gt;&lt;e name=&quot;SignboardHttpsEndpoint&quot; /&gt;&lt;e name=&quot;WebsiteEndpoint&quot; /&gt;&lt;e name=&quot;WebsiteHttpsEndpoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0HttpsCertificate" certificateStore="CA" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/HttpsCertificate" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="HttpsCertificate" />
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApiInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApiUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApiFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="AppCampus.PortalApiUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AppCampus.PortalApiFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AppCampus.PortalApiInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6ef476dd-a809-4e1a-81b5-9bc976a3a1f8" ref="Microsoft.RedDog.Contract\ServiceContract\AppCampus.CloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="c1f74190-bff8-4d2c-9e3d-c9cb06552265" ref="Microsoft.RedDog.Contract\Interface\AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="231d9181-6303-475f-b45e-a645a322eeca" ref="Microsoft.RedDog.Contract\Interface\AppCampus.PortalApi:PortalHttpsEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi:PortalHttpsEndpoint" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="2a5575c9-a0d0-44d3-8846-b1c8ce63a6d7" ref="Microsoft.RedDog.Contract\Interface\AppCampus.PortalApi:SignboardHttpsEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi:SignboardHttpsEndpoint" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="f0c3709d-5ae2-4d83-8873-14b20278834b" ref="Microsoft.RedDog.Contract\Interface\AppCampus.PortalApi:WebsiteEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi:WebsiteEndpoint" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="300e3f42-a8e2-4b73-a97e-4ecb10388dbb" ref="Microsoft.RedDog.Contract\Interface\AppCampus.PortalApi:WebsiteHttpsEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AppCampus.Cloud/AppCampus.CloudGroup/AppCampus.PortalApi:WebsiteHttpsEndpoint" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>