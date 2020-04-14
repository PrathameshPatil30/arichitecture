Feature: GetClinicalDocument
	In order to get patients document details
	As a R1 Chart Master
	I want to retrieve patients document details in specific JSON format

@GetClinicalDocumentDetails
Scenario Outline: To Get Clinical Document Details
	Given I have received Patients Facility Code and Account Number
	When I make API call and pass "<FacilityCode>" and "<AccountNumber>"
	Then The GET API call user receive HTTP response <code>
	And "<responseMessage>" in response body of GET API call

	Examples:
		| FacilityCode | AccountNumber | code | responseMessage                      |
		| SJPR         |               | 400  | The accountNumber field is required. |
		|              | 123456        | 400  | The facilityCode field is required.  |

@GetClinicalDocumentDetails
Scenario Outline: To Get Clinical Document Details from GET API
	Given I have received Patients Facility Code and Account Number
	When I make API call and pass "<FacilityCode>" and "<AccountNumber>"
	Then The GET API call user receive HTTP response <code>
	And  We should receive status "<responseMessage>"

	Examples:
		| FacilityCode | AccountNumber | code | responseMessage  |
		| SJPR         | 987456        | 200  | No records found |
		| ALAL         | 123456        | 200  | No records found |