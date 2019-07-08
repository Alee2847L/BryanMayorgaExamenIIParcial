USE tempdb
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='ERP')
BEGIN
	DROP DATABASE ERP;
END
GO

CREATE DATABASE ERP
GO

use ERP 
go

CREATE SCHEMA Usuarios 
GO

CREATE TABLE Usuarios.usuario
(
	idusuario INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	nombres NVARCHAR(50) NOT NULL,
	apellidos NVARCHAR(50) NOT NULL,
	nombreUsuario NVARCHAR(50) NOT NULL,
	contrasena NVARCHAR(50) NOT NULL,
	correoelectronico NVARCHAR(50) NOT NULL,
	fechacreacion NVARCHAR(10) NOT NULL,
	ultimaconexion NVARCHAR(10) NOT NULL,
	estadoEmpleado VARCHAR (20)  DEFAULT ('ACTIVO/A') NULL,
)
go
/*PROCEDIMIETOS ALMACENADOS
/////////////////////////////////////////////////
/
///////////////////////////////////////////////////
*/
CREATE PROCEDURE AGREGARUSUARIO @nombres NVARCHAR(50), @apellidos NVARCHAR(50), @nombreUsuario VARCHAR(50), @contrasena NVARCHAR(50), @correroelectronico NVARCHAR(50), @fechacreacion NVARCHAR(10), @ultimaconexion NVARCHAR(10) 
AS
begin TRANSACTION
	BEGIN TRY
			INSERT INTO Usuarios.Usuario(nombres, apellidos, nombreUsuario,contrasena,correoelectronico,fechacreacion, ultimaconexion )
			VALUES (@nombres, @apellidos, @nombreUsuario,@contrasena,@correroelectronico, @fechacreacion, @ultimaconexion);
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
GO


--PROCEDIMIENTO DE ACTUALIZAR USUARIO	
CREATE PROCEDURE ACTUALIZARUSUARIO @nombres NVARCHAR(50), @apellidos NVARCHAR(50), @nombreUsuario VARCHAR(50), @contrasena NVARCHAR(50), @correroelectronico NVARCHAR(50), @fechacreacion NVARCHAR(10), @ultimaconexion NVARCHAR(10), @estado VARCHAR(20)
AS
BEGIN TRANSACTION 
	BEGIN TRY
		IF EXISTS(SELECT * FROM Usuarios.usuario WHERE nombres=@nombres) 
		BEGIN
			if exists(SELECT * FROM Usuarios.usuario WHERE nombres=@nombres)
			BEGIN
			UPDATE Usuarios.usuario SET nombres = @nombres, apellidos = @apellidos,nombreUsuario = @nombreUsuario, contrasena = @contrasena, correoelectronico=@correroelectronico, fechacreacion = @fechacreacion, ultimaconexion = @ultimaconexion, estadoEmpleado = @estado WHERE nombres=@nombres;
			END
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
GO


--PROCEDIMIENTO DE ELIMINAR USUARIO	
CREATE PROCEDURE ELIMINARUSUARIO @nombres NVARCHAR(50)
AS
BEGIN TRANSACTION
	BEGIN TRY
			if exists(SELECT * FROM Usuarios.Usuario WHERE nombres=@nombres) --Debe existir un cliente con ese codigo
			BEGIN
					update Usuarios.Usuario SET estadoEmpleado='INACTIVO/A' WHERE nombres=@nombres; --Si el cliente tiene compras no se elimina, solo se le cambia el estado
			END   
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
GO
select *from Usuarios.usuario





