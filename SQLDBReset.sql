delete from MenuDish
go
DBCC CHECKIDENT('MenuDish', RESEED, 0)
go