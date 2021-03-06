use ManejoCitas;

INSERT INTO [dbo].[Carrera]
           ([Nombre])
     VALUES
            ('Psicología')
		   ,('Pedagogía')
		   ,('Psiquiatría');

INSERT INTO [dbo].[Usuario]
           ([Nombre]
           ,[Apellidos]
           ,[Identificacion]
		   ,[Correo]
		   ,[SolicitarCambioPassword]
           ,[RolId]
           ,[Password]
           ,[CarreraId]
		   ,[Eliminado]
           ,[InicioPractica]
           ,[FinPractica]
		    )
     VALUES
           ('Edwin'
           ,'Hernandez S'
           ,'303800795'
		   ,'edhernansol@gmail.com'
		   ,0
           ,1
           ,'123456789'
           ,1
		   ,0
           ,getdate()
           ,getdate()),
		   ('Laura'
           ,'Solano C'
           ,'303800790'
		   ,'edhernansol@gmail.com'
		   ,0
           ,2
           ,'65478991'
           ,1
		   ,0
           ,getdate()
           ,getdate()),
		   ('Elias'
           ,'Hernandez S'
           ,'303800900'
		   ,'edhernansol@gmail.com'
		   ,0
           ,3
           ,'23256549'
           ,1
		   ,0
           ,getdate()
           ,getdate());

INSERT INTO [dbo].[Rol]
           ([Descripcion])
     VALUES
('Administrador'),('Recepcionista'),('Practicante');

INSERT INTO [dbo].[Funcionalidad]
           ([Descripcion]
		   ,[Nombre]
		   ,[Tipo]
           ,[Identificador])
     VALUES 
	 ('Organizacion inicio', 'OrganizacionInicio','Ocultar',1),
	 ('Listado de ofertas', 'ListaOfertas','Ocultar',2),
	 ('Buscar ofertas de otros usuarios','BuscarOfertasUsuarios','Desabilitar', 3),
	 ('Crear ofertas de otros usuarios','CrearOfertasOtrosUsuarios','Desabilitar', 4),
	 ('Eliminar ofertas','EliminarOfertas','Ocultar', 5),
	 ('Buscar citas de otros usuarios','BuscarCitasOtrosUsuarios','Desabilitar', 6),
	 ('Editar informacion paciente','EditarInformacionPaciente','Ocultar', 7),
	 ('Agregar recomendaciones paciente', 'AgregarRecomendacionesPaciente','Ocultar', 8),
	 ('Cambiar horario cita', 'CambiarHorarioCita','Ocultar', 9),
	 ('Eliminar citas paciente', 'EliminarCitasPaciente','Ocultar', 10),
	 ('Listado de usuarios', 'ListadoUsuarios','Ocultar', 11);

----Rol de adminsitrador
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values
(1,1),--Organizacion inicio, 1
(1,2),--Listado de ofertas, 2
(1,3),--Buscar ofertas de otros usuarios 3
(1,4),--Crear ofertas de otros usuarios, 4
(1,5),--Eliminar ofertas, 5
(1,6),--Buscar citas de otros usuarios, 6
(1,7),--Editar informacion paciente, 7
(1,8),--Agregar recomendaciones paciente, 8
(1,9),--Cambiar horario cita, 9
(1,10),--Eliminar citas paciente, 10
(1,11);--Listado de usuarios, 11

----Rol de recepcionista
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values 
(2,6),--Buscar citas de otros usuarios, 6
(2,7),--Editar informacion paciente, 7
(2,8),--Agregar recomendaciones paciente, 8
(2,9),--Cambiar horario cita, 9
(2,10);--Eliminar citas paciente, 10


----Rol de practicante
Insert Into [dbo].[Rol_Funcionalidad]([RolId],[FuncionalidadId])
Values 
(3,2),--Listado de ofertas, 2
(3,5),--Eliminar ofertas, 5
(3,7),--Editar informacion paciente, 7
(3,8),--Agregar recomendaciones paciente, 8
(3,9),--Cambiar horario cita, 9
(3,10);--Eliminar citas paciente, 10