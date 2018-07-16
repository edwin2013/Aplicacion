use ManejoCitas;

INSERT INTO [dbo].[Usuario]
           ([Nombre]
           ,[Apellidos]
           ,[Identificacion]
           ,[RolId]
           ,[Password]
           ,[CarreraId]
           ,[InicioPractica]
           ,[FinPractica])
     VALUES
           ('Edwin'
           ,'Hernandez S'
           ,'303800795'
           ,1
           ,'123456789'
           ,1
           ,getdate()
           ,getdate()),
		   ('Laura'
           ,'Solano C'
           ,'303800790'
           ,2
           ,'65478991'
           ,1
           ,getdate()
           ,getdate()),
		   ('Elias'
           ,'Hernandez S'
           ,'303800900'
           ,3
           ,'23256549'
           ,1
           ,getdate()
           ,getdate());

INSERT INTO [dbo].[Rol]
           ([Descripcion])
     VALUES
('Administrador'),('Recepcionista'),('Practicante');

INSERT INTO [dbo].[Funcionalidad]
           ([Descripcion]
           ,[Identificador])
     VALUES 
	 ('Paciente inicio', 1),
	 ('Listado de ofertas', 2),
	 ('Buscar ofertas propias', 3),
	 ('Buscar ofertas de otros usuarios', 4),
	 ('Crear ofertas propias', 5),
	 ('Crear ofertas de otros usuarios', 6),
	 ('Eliminar ofertas', 7),
	 ('Listado de citas', 8),
	 ('Buscar citas propias', 9),
	 ('Buscar citas de otros usuarios', 10),
	 ('Actualizar informacion paciente', 11),
	 ('Agregar recomendaciones paciente', 12),
	 ('Eliminar citas paciente', 13);

----Rol de adminsitrador
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values (1,1),
(1,1),--Paciente inicio, 1
(1,2),--Listado de ofertas, 2
(1,3),--Buscar ofertas propias, 3
(1,4),--Buscar ofertas de otros usuarios, 4
(1,5),--Crear ofertas propias, 5
(1,6),--Crear ofertas de otros usuarios, 6
(1,7),--Eliminar ofertas, 7
(1,8),--Listado de citas, 8
(1,9),--Buscar citas propias, 9
(1,10),--Buscar citas de otros usuarios, 10
(1,11),--Actualizar informacion paciente, 11
(1,12),--Agregar recomendaciones paciente, 12
(1,13);--Eliminar citas paciente, 13

----Rol de recepcionista
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values (1,1),
(2,1),--Paciente inicio, 1
--(2,2),--Listado de ofertas, 2
--(2,3),--Buscar ofertas propias, 3
--(2,4),--Buscar ofertas de otros usuarios, 4
--(2,5),--Crear ofertas propias, 5
--(2,6),--Crear ofertas de otros usuarios, 6
--(2,7),--Eliminar ofertas, 7
(2,8),--Listado de citas, 8
(2,9),--Buscar citas propias, 9
(2,10),--Buscar citas de otros usuarios, 10
(2,11)--Actualizar informacion paciente, 11
--(2,12),--Agregar recomendaciones paciente, 12
--(2,13);--Eliminar citas paciente, 13

----Rol de practicante
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values (1,1),
(1,1),--Paciente inicio, 1
(1,2),--Listado de ofertas, 2
(1,3),--Buscar ofertas propias, 3
--(1,4),--Buscar ofertas de otros usuarios, 4
(1,5),--Crear ofertas propias, 5
--(1,6),--Crear ofertas de otros usuarios, 6
(1,7),--Eliminar ofertas, 7
(1,8),--Listado de citas, 8
(1,9),--Buscar citas propias, 9
--(1,10),--Buscar citas de otros usuarios, 10
--(1,11),--Actualizar informacion paciente, 11
(1,12),--Agregar recomendaciones paciente, 12
(1,13)--Eliminar citas paciente, 13