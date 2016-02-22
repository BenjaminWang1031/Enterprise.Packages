@Dll Name: Enterprise.Component.Log4DNET
@Version: 0.0.0.1
@Author: Infosys
@Dec: This is a common log component which was built based on log4Net.
@Has been used in following projects: OASIS/Mounting Kits/...


**************************************************************************************************************
1. Add following section to web.config/configuration/configSections section
**************************************************************************************************************

<!--Log4net-->
<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net" />

**************************************************************************************************************
2. Add following section to web.config/configuration section, you can only add the appender which you need.
**************************************************************************************************************

<log4net debug="false">
    <!--Exception-->
    <appender name="ExceptionLog" type="log4net.Appender.RollingFileAppender" LEVEL="WARN">
      <encoding value="utf-8" />
      <param name="File" value="Log\Exception.txt" />
      <param name="datePattern" value="MM-dd HH:mm" />
      <param name="AppendToFile" value="true" />
      <param name="MaxSizeRollBackups" value="1000000" />
      <param name="MaximumFileSize" value="4MB" />
      <param name="RollingStyle" value="Size" />
      <param name="StaticLogFileName" value="true" />
      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="levelMin" value="WARN" />
        <param name="levelMax" value="WARN" />
      </filter>
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%m%n" />
        <!--[%t] [%class.%method]-->
      </layout>
    </appender>
    <!--Big Queries-->
    <appender name="QueryLog" type="log4net.Appender.RollingFileAppender" LEVEL="FATAL">
      <encoding value="utf-8" />
      <param name="File" value="Log\Query.txt" />
      <param name="datePattern" value="MM-dd HH:mm" />
      <param name="AppendToFile" value="true" />
      <param name="MaxSizeRollBackups" value="1000000" />
      <param name="MaximumFileSize" value="4MB" />
      <param name="RollingStyle" value="Size" />
      <param name="StaticLogFileName" value="true" />
      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="levelMin" value="FATAL" />
        <param name="levelMax" value="FATAL" />
      </filter>
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%m%n" />
        <!--[%t] [%class.%method]-->
      </layout>
    </appender>
    <!--Users-->
    <appender name="UsersLog" type="log4net.Appender.RollingFileAppender" LEVEL="ERROR">
      <encoding value="utf-8" />
      <param name="File" value="Log\Users.txt" />
      <param name="datePattern" value="MM-dd HH:mm" />
      <param name="AppendToFile" value="true" />
      <param name="MaxSizeRollBackups" value="1000000" />
      <param name="MaximumFileSize" value="4MB" />
      <param name="RollingStyle" value="Size" />
      <param name="StaticLogFileName" value="true" />
      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="levelMin" value="ERROR" />
        <param name="levelMax" value="ERROR" />
      </filter>
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%m%n" />
        <!--[%t] [%class.%method]-->
      </layout>
    </appender>
    <root>
      <level value="WARN" />
      <appender-ref ref="ExceptionLog" />
      <appender-ref ref="QueryLog" />
      <appender-ref ref="UsersLog" />
    </root>
  </log4net>

**************************************************************************************************************
3. When deploy your web app to IIS, need to set permissions to the log folder
'TODO:List all the web users and permissions here....
**************************************************************************************************************