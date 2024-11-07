CREATE TABLE Helado (
	IdHelado int identity(1,1) not null primary key,
	Descripcion varchar(100) not null, 
	Kilos int,
	IdUsuarioAlta int not null, 
	FechaAlta datetime not null, 
	IdUsuarioModificacion int null, 
	FechaModificacion datetime null, 
	IdUsuarioBaja int null,
	FechaBaja datetime null
)

CREATE TABLE Usuario(
	IdUsuario int identity(1,1) not null primary key,
	NombreUsuario varchar(100) not null,
	MailUsuario varchar(100) not null unique	
)

select * from Usuario
select * from Helado
