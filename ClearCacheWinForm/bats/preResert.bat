@echo off
REM Զ�̵�ַ
set ycip=127.0.0.1
REM ��������½��
set ycname=Administrator
REM ��������½����
set ycpwd=123456
REM �ƻ���������
set planName=IIS_Resert_1
REM ipc����
set ipcName=ipc
set ycpcname=pcname

REM ִ�нű���ַ
set execbat=\\%ycip%\D$\execiisresest.bat
set taskPath=D:\execiisresest.bat
echo ����Զ������
net use \\%ycip% "%ycpwd%" /user:%ycpcname%\%ycname%

REM echo д��ű���Զ�̽ű�
REM ....д��־ δд�������Ȩ������
 echo echo %time%ִ������^>^>%taskPath%.txt>%execbat%
REM ....����IIS
 echo iisreset>>%execbat%
REM ....ɾ���ƻ����� echo yes| ʼ��ȷ��
 echo echo yes^| SCHTASKS /Delete /TN "%planName%" /F>>%execbat%
REM ����ʱ���ӳ�
set tasktime="00:00"

echo ��Զ�����������ƻ�����

SCHTASKS /Create /S %ycip% /U %ycpcname%\%ycname% /P "%ycpwd%" /SC ONCE /ST %tasktime% /TN %planName% /TR %taskPath% /RU %ycname% /F

echo ����ִ�ж���ļƻ�����
SCHTASKS /Run /S %ycip% /U %ycpcname%\%ycname% /P "%ycpwd%" /I /TN "%planName%"
REM ɾ��IPC����
net use \\%ycip% /del
pause
