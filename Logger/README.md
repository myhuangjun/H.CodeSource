# 日志系统(NLog)

#### 步骤

1. 引入NLog.Extensions.Logging包

2. 添加nlog.config配置文件 **复制到输出目录**

3. 注册服务并且配置NLog

   `logBuild.AddLogging(logBuilder =>{logBuilder.AddNLog();})`

4. 使用类中 `ILogger<T> _logger`来创建日志对象



#### 附件

*nlog.config配置文件*

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

将注释去掉，最精简版本如下

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
  <targets>
    <!--输出目标:name名称f,xsi:type输出类型文件, fileName输出到程序根目录logs文件夹中, 以日期作为生成log文件名称, layout生成内容的格式 archiveAboveSize:最大文件大小
    maxArchiveFiles:最多保存指定数量的文件-->
    <target name="f"
            xsi:type="File"
            fileName="${basedir}/logs/${shortdate}.log"
            archiveAboveSize="10000"
            layout="${longdate} ${uppercase:${level}} ${message}" />
  <rules>
      <!--日志路由规则：最低级别Debug，输出到target目标f-->
    <logger name="*" minlevel="Debug" writeTo="f" />
  </rules>
</nlog>
```

`

#### 扩展

*结构化日志,NLog配置麻烦,推荐使用Serilog*

*集中日志服务推荐使用:Exceptionless*
