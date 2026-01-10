drop table QCResultEntries
drop table Schedulers
drop table TestControlSamples
drop table ControlMasters
drop table Reagents

delete from ControlMasters
delete from QCResultEntries
delete from Schedulers
delete from TestControlSamples
delete from Reagents

select * from ControlMasters
select * from QCResultEntries
select * from Schedulers
select * from TestControlSamples
select * from Reagents