# ��־ϵͳ(NLog)

#### ����

1. ����NLog.Extensions.Logging��

2. ���nlog.config�����ļ� **���Ƶ����Ŀ¼**

3. ע�����������NLog

   `logBuild.AddLogging(logBuilder =>{logBuilder.AddNLog();})`

4. ʹ������ `ILogger<T> _logger`��������־����



#### ����

*nlog.config�����ļ�*

`

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      autoReload="true"
      throwExceptions="false"
      internalLogLevel="Off" internalLogFile="c:\temp\nlog-internal.log">

  <!-- optional, add some variables
  https://github.com/nlog/NLog/wiki/Configuration-file#variables
  -->
  <variable name="myvar" value="myvalue"/>

  <!--
  See https://github.com/nlog/nlog/wiki/Configuration-file
  for information on customizing logging rules and outputs.
   -->
  <targets>

    <!--
    add your targets here
    See https://github.com/nlog/NLog/wiki/Targets for possible targets.
    See https://github.com/nlog/NLog/wiki/Layout-Renderers for the possible layout renderers.
    -->

    <!--
    Write events to a file with the date in the filename.
    <target xsi:type="File" name="f" fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate} ${uppercase:${level}} ${message}" />
    -->
  </targets>

  <rules>
    <!-- add your logging rules here -->

    <!--
    Write all events with minimal level of Debug (So Debug, Info, Warn, Error and Fatal, but not Trace)  to "f"
    <logger name="*" minlevel="Debug" writeTo="f" />
    -->
  </rules>
</nlog>
```

��ע��ȥ�������汾����

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
  <targets>
    <!--���Ŀ��:name����f,xsi:type��������ļ�, fileName����������Ŀ¼logs�ļ�����, ��������Ϊ����log�ļ�����, layout�������ݵĸ�ʽ archiveAboveSize:����ļ���С
    maxArchiveFiles:��ౣ��ָ���������ļ�-->
    <target name="f"
            xsi:type="File"
            fileName="${basedir}/logs/${shortdate}.log"
            archiveAboveSize="10000"
            layout="${longdate} ${uppercase:${level}} ${message}" />
  <rules>
      <!--��־·�ɹ�����ͼ���Debug�������targetĿ��f-->
    <logger name="*" minlevel="Debug" writeTo="f" />
  </rules>
</nlog>
```

`

#### ��չ

*�ṹ����־,NLog�����鷳,�Ƽ�ʹ��Serilog*

*������־�����Ƽ�ʹ��:Exceptionless*
