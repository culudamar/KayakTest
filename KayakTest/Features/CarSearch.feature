Feature: Car Search
	Car Search functions on the main page

@mytag
Scenario: Simple search
	Given the main page is open
	When I enter Istanbul in From field
	And I select first tip
	And I hit search button
	Then the result page should open

Scenario: No location
	Given the main page is open
	When I hit search button
	Then I see error Please pick a pick-up location.

Scenario: No drop-off location
	Given the main page is open
	When I select different drop-off
	And I enter Istanbul in From field
	And I select first tip
	And I hit search button
	Then I see error Please pick a drop-off location.