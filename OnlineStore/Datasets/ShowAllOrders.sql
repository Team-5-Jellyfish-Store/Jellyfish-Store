SELECT u.Username, o.Comment, o.OrderedOn, p.Name, p.SellingPrice, op.ProductCount
FROM Orders o
	JOIN Users u
		ON o.UserId = u.Id
	JOIN OrderProducts op
		ON op.OrderId = o.Id
	JOIN Products p
		ON op.ProductId = p.Id