Feature: ProductCategory

A short summary of the feature

Feature: ProductCategory

A short summary of the feature

@ProductCategory
	Scenario: Creating A New Product Category
	Given I Want To Create the Product Category 'SelfEsteemFiles'
	When I Press Add Button
	Then I can see 'SelfEsteemFiles' gets Available On the List
	
@ProductCategory
Scenario: Duplicated Product Category Cant Be Created
	Given I Have Already Created PrdocutCategory 'Management Packages'
	When I Try To Create It Again
	Then I should Receive According Message About It
	And 'Management Packages' Should Not Be Appeared In List Twice
	

