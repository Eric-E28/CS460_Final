-- Seed Station table
INSERT INTO Station (StationName) VALUES 
('Kitchen'),
('Dessert'),
('Drinks');

INSERT INTO Store (StoreName) VALUES 
('Doughnut Dreams & Brewed Beans');

INSERT INTO Dlvy (DlvyName) VALUES 
('Main Counter'),
('Drive Thru'),
('Walk-in Counter');

-- Seed Item table
INSERT INTO Item (Name, Description, Price, StationID) VALUES 
-- Kitchen Station
('Breakfast Burrito', 'Scrambled eggs, cheese, and veggies in a tortilla', 5.49, 1),
('Veggie Wrap', 'Fresh vegetables and hummus in a whole-wheat wrap', 6.49, 1),
('Mediterranean Wrap', 'Hummus, falafel, and fresh veggies in a wrap', 6.99, 1),
('Turkey & Avocado Panini', 'Turkey, avocado, and Swiss cheese on grilled ciabatta', 6.49, 1),
('Caprese Salad', 'Fresh tomatoes, mozzarella, and basil drizzled with balsamic', 7.49, 1),
('Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 5.99, 1),
('Quinoa Salad', 'Quinoa, mixed vegetables, and lemon vinaigrette', 6.99, 1),
('BBQ Pulled Pork Sandwich', 'Slow-cooked pulled pork with barbecue sauce', 6.99, 1),
('Fruit Explosion Bowl', 'Fresh mixed fruits with a drizzle of honey', 6.99, 1),

-- Dessert Station
('Glazed Doughnut', 'Classic ring-shaped doughnut with sweet glaze', 2.49, 2),
('Blueberry Muffin', 'Moist muffin bursting with blueberries', 3.49, 2),
('Nutella-Filled Donut', 'Soft donut filled with luscious Nutella', 3.99, 2),
('Cinnamon Roll', 'Sweet and gooey cinnamon-swirl pastry', 3.99, 2),
('Chocolate Croissant', 'Buttery croissant with a chocolate filling', 3.49, 2),
('Apple Fritter', 'Deep-fried pastry filled with spiced apples', 3.99, 2),
('Latte Macaron', 'Espresso and steamed milk with a sweet macaron', 4.49, 2),
('Lemon Poppy Seed Cake', 'Zesty lemon cake with poppy seeds', 3.99, 2),
('Cinnamon Sugar Donut', 'Fluffy donut dusted with cinnamon and sugar', 2.99, 2),
('Raspberry Danish', 'Flaky pastry filled with sweet raspberry jam', 3.49, 2),
('Chocolate Chip Scone', 'Buttery scone with chocolate chips', 3.99, 2),
('S''mores Donut', 'Donut filled with marshmallow and chocolate', 3.49, 2),
('Red Velvet Cupcake', 'Decadent red velvet cupcake with cream cheese frosting', 3.49, 2),
('Almond Croissant', 'Flaky croissant with a delightful almond filling', 3.99, 2),
('Greek Yogurt Parfait', 'Layers of yogurt, granola, and fresh berries', 4.49, 2),


-- Espresso Station
('Caramel Macchiato', 'Rich espresso with caramel and steamed milk', 4.99, 3),
('Mocha Frappuccino', 'Creamy coffee-chocolate blend with whipped cream', 5.99, 3),
('Espresso Shot', 'A quick pick-me-up of pure espresso', 1.99, 3),
('Chai Latte', 'Spiced black tea with steamed milk and foam', 4.49, 3),
('Decaf Latte', 'Smooth decaffeinated latte with frothy milk', 4.99, 3),
('Americano', 'Strong black coffee with hot water', 2.99, 3),
('Affogato', 'Vanilla ice cream drowned in a shot of espresso', 5.99, 3),
('Iced Chai Latte', 'Chilled spiced tea with milk and ice', 4.99, 3),
('Hazelnut Latte', 'Rich latte with a nutty twist of hazelnut', 4.99, 3),
('Hot Chocolate', 'Creamy and indulgent hot chocolate', 4.49, 3),
('Iced Matcha Latte', 'Iced green tea latte with a hint of matcha', 4.99, 3),
('Iced Green Tea', 'Refreshing green tea served over ice', 3.99, 3),
('Cold Brew', 'Smooth and strong cold-brewed coffee', 4.49, 3),
('Chocolate Milkshake', 'Rich chocolate shake topped with whipped cream', 5.49, 3),
('Smoothie Sensation', 'Blended fruit smoothie with yogurt and honey', 4.99, 3),
('Pomegranate Paradise', 'Pomegranate and berry smoothie with a hint of mint', 5.49, 3);
