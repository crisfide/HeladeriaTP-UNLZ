ALTER TABLE Usuario
ADD GoogleIdentificador NVARCHAR(255);

select * from Usuario

CREATE TABLE Pedido (
    idPedido INT PRIMARY KEY IDENTITY(1,1),  -- Identificador único y auto-incremental
    Kilos INT CHECK (Kilos >= 0) NOT NULL,   -- Cantidad de kilos, no negativo y requerido
    idHelado INT NOT NULL,                   -- ID del helado en el pedido, requerido
    IdUsuarioAlta INT NOT NULL               -- ID del usuario que creó el pedido, requerido
);

ALTER TABLE Pedido
ADD CONSTRAINT FK_Pedido_Helado FOREIGN KEY (idHelado) REFERENCES Helado(IdHelado),
    CONSTRAINT FK_Pedido_UsuarioAlta FOREIGN KEY (IdUsuarioAlta) REFERENCES Usuario(IdUsuario);



select * from Usuario, Helado
select * from Grupo6.Pedido
