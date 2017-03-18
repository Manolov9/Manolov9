Feature: LoginUSER
	The test will provide information of 
	system with Login from Admin and check him in step defined from us!!!

@AdminLogin
Scenario: Check Login Panel with correct Admin user
	Given Setup
	And Fillinformationfromtheuser
	When Checksteps
	Then Ifallstepsarecorrectlycompletedthendriverwillclosethebrowser
