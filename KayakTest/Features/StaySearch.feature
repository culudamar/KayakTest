Feature: Stay Search
	Stay Search functions on the main page

@mytag
Scenario: Simple search
	Given the main page is open
	When I enter Istanbul in From field
	And I select first tip
	And I hit search button
	Then the result page should open

Scenario: No location
	Given the main page is open
	When I click Stays
	And I hit search button
	Then I see error Please enter a city, hotel name, or landmark.