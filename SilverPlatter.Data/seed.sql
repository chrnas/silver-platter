USE SilverPlatterDB;

INSERT INTO Restaurants (Name, Description, Rating, Budget, Address) VALUES
('The Golden Spoon', 'Fine dining with a cozy atmosphere', 3, 400, '123 Main Street'),
('Ocean Breeze Cafe', 'Seafood specialties with ocean view', 1, 100, '456 Ocean Drive'),
('Urban Eats', 'Casual meals for city lovers', 5, 200, '789 Downtown Blvd');

INSERT INTO MenuEntries (Name, Description, Allergy, RestaurantId) VALUES
('Grilled Salmon', 'Fresh salmon with lemon butter sauce', 'Gluten', 2),
('Cheeseburger', 'Classic beef burger with cheddar cheese', 'Vegan', 3),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 'Vegetarian', 1),
('Spaghetti Carbonara', 'Pasta with pancetta and creamy sauce', '', 1),
('Lobster Roll', 'Fresh lobster in a toasted bun', '', 2);

INSERT INTO BookingTables (Name, Description, Places, RestaurantId) VALUES
('Table 1', 'Near window with a view', 4, 1),
('Table 2', 'Cozy corner', 2, 1),
('Table 1', 'Sea view', 4, 2),
('Table 2', 'Near entrance', 2, 2),
('Table 1', 'Central area', 6, 3),
('Table 2', 'Quiet corner', 4, 3);

INSERT INTO Users (Name, Budget, PreferedRating) VALUES
('John Testing',  400, 3);

INSERT INTO RestaurantFavorites (UserId, RestaurantId) VALUES 
(1, 1);

INSERT INTO Allergies (Name, UserId) VALUES 
('Gluten', 1);