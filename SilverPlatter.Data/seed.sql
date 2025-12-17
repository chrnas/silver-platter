USE SilverPlatterDB;

INSERT INTO Restaurants (Name, Description, Rating, Budget, Address) VALUES
('The Golden Spoon', 'Fine dining with a cozy atmosphere', 3, 400, '123 Main Street'),
('Ocean Breeze Cafe', 'Seafood specialties with ocean view', 1, 100, '456 Ocean Drive'),
('Urban Eats', 'Casual meals for city lovers', 5, 200, '789 Downtown Blvd'),
('Flight Bar', 'High adrenaline meals for frequent fliers', 3, 150, '432 Airport Lane'),
('Student Foods', 'Affordable meals for students on a budget', 2, 50, '224 Algebra Highway'),
('Jones Steakhouse', 'Only the best ribs for the best people', 4, 300, '555 BBQ Blvd');

INSERT INTO MenuEntries (Name, Description, Allergy, RestaurantId) VALUES
('Grilled Salmon', 'Fresh salmon with lemon butter sauce', 'Gluten', 2),
('Cheeseburger', 'Classic beef burger with cheddar cheese', 'Vegan', 3),
('Cheeseburger', 'Classic beef burger with cheddar cheese', 'Vegan', 4),
('Cheeseburger', 'Classic beef burger with cheddar cheese', '', 6),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 'Vegetarian', 1),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 'Vegetarian', 5),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 'Vegetarian', 2),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 'Vegetarian', 3),
('Spaghetti Carbonara', 'Pasta with pancetta and creamy sauce', '', 1),
('Lobster Roll', 'Fresh lobster in a toasted bun', '', 2),
('Wings', 'Seasoned chicken wings served with mashed potatoes', '', 4),
('100 Octane Margheritas', 'A drink sure to make it feel like you`re flying sky high', 'Vegan', 4),
('Mac & Cheese', 'A basic food all students will recognize and enjoy', 'Gluten', 5),
('Struggle Friec Rice', 'Fried rice made with yesterdays rice and whatever else the chef could find', '', 5),
('Ribs', 'Just ribs, the best for sure', 'Vegetarian', 6),
('Steak Tartare', 'Handcarved Ox, dijonmustard and eggs', '', 6);


INSERT INTO BookingTables (Name, Description, Places, Booked, RestaurantId) VALUES
('Table 1', 'Near window with a view', 4, 0, 1),
('Table 2', 'Cozy corner', 2, 0, 1),
('Table 1', 'Sea view', 4, 0, 2),
('Table 2', 'Near entrance', 2, 0, 2),
('Table 1', 'Central area', 6, 0, 3),
('Table 2', 'Quiet corner', 4, 0, 3);

INSERT INTO Users (Name, Budget, PreferedRating) VALUES
('John Testing',  400, 3);

INSERT INTO RestaurantFavorites (UserId, RestaurantId) VALUES 
(1, 1);

INSERT INTO Allergies (Name, UserId) VALUES 
('Gluten', 1);