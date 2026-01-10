
drop table BloodBankInventories
drop table BloodSampleInventories
drop table BloodSamples
drop table BloodSampleResults
drop table RegistrationTransactions
drop table RegisteredProducts;
drop table RegisteredSpecialRequirements
drop table BloodBankRegistrations;
drop table BloodBankPatients;

delete from BloodSampleInventories
delete from BloodSamples
delete from BloodSampleResults
delete from RegistrationTransactions
delete from RegisteredProducts;
delete from RegisteredSpecialRequirements
delete from BloodBankRegistrations;
delete from BloodBankPatients;

select * from BloodBankInventories
select * from BloodBankInventoryTransactions
select * from BloodSampleInventories
select * from BloodSamples
select * from BloodSampleResults
select * from RegistrationTransactions
select * from RegisteredProducts;
select * from BloodBankRegistrations;
select * from RegisteredSpecialRequirements
select * from BloodBankPatients;


select * from BloodBankInventories
select * from BloodBankInventoryTransactions
select * from BloodSampleInventories where RegistrationId = 165
select * from BloodSamples where RegistrationId = 165
select * from BloodSampleResults where BloodBankRegistrationId = 165
select * from RegistrationTransactions where RegistrationId = 165
select * from RegisteredProducts where BloodBankRegistrationId = 165
select * from BloodBankRegistrations where RegistrationId = 165
select * from RegisteredSpecialRequirements where RegistrationId = 165
select * from BloodBankPatients;

drop table ProductSpecialRequirements ;
drop table Products ;
drop table Lookups ;
drop table tariffs;


select * from categories; 
select * from specialrequirements;
select * from lookups; 
select * from products;
select * from tariffs;
