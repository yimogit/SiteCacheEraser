@echo off
REM 远程地址
set ycip=127.0.0.1
REM 服务器登陆名
set ycname=Administrator
REM 服务器登陆密码
set ycpwd=123456
REM 计划任务名称
set planName=IIS_Resert_1
REM ipc名称
set ipcName=ipc
set ycpcname=pcname

REM 执行脚本地址
set execbat=\\%ycip%\D$\execiisresest.bat
set taskPath=D:\execiisresest.bat
echo 建立远程连接
net use \\%ycip% "%ycpwd%" /user:%ycpcname%\%ycname%

REM echo 写入脚本到远程脚本
REM ....写日志 未写入可能是权限问题
 echo echo %time%执行任务^>^>%taskPath%.txt>%execbat%
REM ....重启IIS
 echo iisreset>>%execbat%
REM ....删除计划任务 echo yes| 始终确认
 echo echo yes^| SCHTASKS /Delete /TN "%planName%" /F>>%execbat%
REM 任务时间延迟
set tasktime="00:00"

echo 在远程主机创建计划任务

SCHTASKS /Create /S %ycip% /U %ycpcname%\%ycname% /P "%ycpwd%" /SC ONCE /ST %tasktime% /TN %planName% /TR %taskPath% /RU %ycname% /F

echo 立即执行定义的计划任务
SCHTASKS /Run /S %ycip% /U %ycpcname%\%ycname% /P "%ycpwd%" /I /TN "%planName%"
REM 删除IPC连接
net use \\%ycip% /del
pause
