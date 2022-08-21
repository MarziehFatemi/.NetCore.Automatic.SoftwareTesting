Feature: Product

A short summary of the feature

@tag1
Scenario: Creating a Product
	Given I have already created a product category 'Business'
	When I want to create a product with UnitPrice '110' name 'Management Skill' in this Category
	Then I must see the product category 'Business' in the list 
	And I can create the product 
	And See the product with UnitPrice '110' name 'Management Skill' and productCategory 'Business' available on the list 


	
	Scenario: having error when the product category and product name is repeated 
	Given I have already created a product category 'Business'
	And  I have already created a product with UnitPrice '110' and name 'Management Skill' in this category
	When I want to create a product with UnitPrice '1100' and name 'Management Skill' in this category
	Then I should Error Repeatitive Message About It 
	And I should not see the product with product category 'Business' and name 'Management Skill' twice 
	
	

