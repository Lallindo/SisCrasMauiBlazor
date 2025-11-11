INSERT INTO Cras (Nome)
VALUES ('Central'),
       ('Dona Tita'),
       ('Pedro Ometto'),
       ('Cila de Bauab'),
       ('Altos da Cidade'),
       ('Distrito de Potunduva');

INSERT INTO Tecnicos (Nome, Login, Senha)
VALUES ('Bruno', 'bruno', '$2a$12$AZ2wBvPEeG.zcDHmaZ5eg.6xBwRKKmNObIQPEPDabL.amNKYRgPE.'),
       ('José', 'jose', '$2a$12$WScILWrN2.t5f3UBgbm9aee0Iea.k0gVuR7GTHPE7fTdcmZXJzh12');

INSERT INTO Usuarios(Nome, Cpf, DataNascimento, Escolaridade, EstadoCivil, FonteRenda, Nis, NomeSocial, Ocupacao,
                     OrientacaoSexual, Profissao, Raca, RendaBruta, Rg, Sexo)
Values ('Ana', '12345678910', '08-04-2002', 1, 2,
        1, '12345678911', '', 'Programador', 1,
        'Programador', 1, 1000.00, '12345678912', 1),
       ('João', '12345678913', '17-02-1998', 1, 2,
        1, '12345678914', '', 'Testador', 1,
        'Testador', 3, 2000.00, '12345678915', 0);

INSERT INTO Familias(ConfiguracaoFamiliar)
VALUES (1),
       (0),
       (3);

INSERT INTO Prontuarios(Codigo, CrasId, DataCriacao, FamiliaId, FormaDeAcesso, TecnicoId)
VALUES (1, 1, '03-11-2025', 1, 'Testando', 1),
       (2, 2, '04-10-2025', 2, 'Testando2', 2);

INSERT INTO TecnicoCras(CrasId, DataEntrada, TecnicoId)
VALUES (1, '04-11-2023', 1),
       (2, '16-10-2022', 2);

INSERT INTO FamiliaUsuarios(Ativo, Familiaid, Parentesco, UsuarioId)
VALUES (1, 1, 'T1', 1),
       (1, 1, 'T2', 2);