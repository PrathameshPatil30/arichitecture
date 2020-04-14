Feature: SaveClinicalDocument
	In order to save patients document
	As a R1 Chart Master
	I want to consume patients document details in specific JSON format

@SaveClinicalDocumentDetails
Scenario: To Save Clinical Document Details
	Given I have received Patients Clinical Document Details
	When I consume JSON and make API call
	Then the result should be saved in database
	And We receive response message in response body

@SaveClinicalDocumentDetails
Scenario Outline: Validations in request JSON
	Given I have request JSON for saving clinical documents with "<parameter>" "<value>"
	When I call the API service
	Then user receive HTTP response <code>
	And "<responseMessage>" in response body

	Examples:
		| parameter     | value | code | responseMessage            |
		| AccountNumber |       | 400  | Account Number is required |
		| FacilityCode  |       | 400  | Facility Code is required  |
		| DocumentType  |       | 400  | Document Type is required  |
		| DocumentName  |       | 400  | Document Name is required  |

@SaveClinicalDocumentDetails
Scenario Outline: Validations in request JSON with invalid inputs
	Given I have request JSON for saving clinical documents in API with "<parameter>" "<value>"
	When I call the Clinical Document API service
	Then user receives the HTTP response <code>
	And "<responseMessage>" in the response body

	Examples:
		| parameter     | value                                                                                                    | code | responseMessage                                       |
		| AccountNumber | !@#                                                                                                      | 400  | Please enter valid Account Number                     |
		| FacilityCode  | !@#!                                                                                                     | 400  | Please enter valid Facility Code                      |
		| DocumentType  | !@#                                                                                                      | 400  | Please enter valid Document Type                      |
		| DocumentType  | abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd | 400  | Document Type should not be more than 50 characters  |
		| DocumentName  | abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd | 400  | Document Name should not be more than 100 characters  |
		| AccountNumber | abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd | 400  | Account Number should not be more than 20 characters |
		| FacilityCode  | abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd | 400  | Facility Code must be 4 characters only               |