INSERT INTO Cras (Nome)
VALUES ('Central'),
       ('Dona Tita'),
       ('Pedro Ometto'),
       ('Cila de Bauab'),
       ('Altos da Cidade'),
       ('Distrito de Potunduva');

INSERT INTO Tecnicos (Nome, Login, Senha)
VALUES ('Bruno', 'bruno', '$2a$12$AZ2wBvPEeG.zcDHmaZ5eg.6xBwRKKmNObIQPEPDabL.amNKYRgPE.'),
       ('José', 'jose', '$2a$12$WScILWrN2.t5f3UBgbm9aee0Iea.k0gVuR7GTHPE7fTdcmZXJzh12'),
       ('Amanda Silva', 'amanda', '$2a$12$AZ2wBvPEeG.zcDHmaZ5eg.6xBwRKKmNObIQPEPDabL.amNKYRgPE.'),
       ('Roberto Carlos', 'roberto', '$2a$12$AZ2wBvPEeG.zcDHmaZ5eg.6xBwRKKmNObIQPEPDabL.amNKYRgPE.');

INSERT INTO Usuarios(Nome, Cpf, DataNascimento, Escolaridade, EstadoCivil, FonteRenda, Nis, NomeSocial, Ocupacao,
                     OrientacaoSexual, Profissao, Raca, RendaBruta, Rg, Sexo)
Values ('Ana', '12345678910', '04-08-2002', 1, 2,
        1, '12345678911', '', 'Programador', 1,
        'Programador', 1, 1000.00, '12345678912', 1),
    
       ('João', '12345678913', '02-17-1998', 1, 2,
        1, '12345678914', '', 'Testador', 1,
        'Testador', 3, 2000.00, '12345678915', 0),
    
       ('Maria', '12345678916', '12-23-1997', 5, 1,
        1, '12345678917', '', 'Analista', 3,
        'Analista', 2, 3000.00, '12345678918', 1),

        ('Carlos Silva', '98765432100', '05-15-1985', 3, 1, 
        1, '98765432101', '', 'Pedreiro', 1, 
        'Pedreiro', 5, 2500.00, '223334445', 1), 

       ('Fernanda Silva', '98765432102', '08-20-1988', 3, 1, 
        0, '98765432103', '', 'Do Lar', 1, 
        'Do Lar', 2, 0.00, '223334446', 2), 

       ('Lucas Silva', '98765432104', '01-10-2015', 1, 6,
        0, '98765432105', '', 'Estudante', 0, 
        'Estudante', 5, 0.00, '223334447', 1),

       ('Patrícia Santos', '11122233344', '03-12-1990', 4, 6, 
        1, '11122233345', '', 'Vendedora', 1,
        'Vendedora', 1, 1800.00, '556667778', 2),

       ('Beatriz Santos', '11122233346', '07-25-2018', 1, 6,
        0, '11122233347', '', 'Estudante', 0,
        'Estudante', 1, 0.00, '556667779', 2),
    
       ('Antônio Gonçalves', '55544433322', '06-15-1950', 3, 6,
        1, '12345678999', '', 'Aposentado', 1,
        'Aposentado', 2, 1320.00, '998877665', 1), 

       ('Juliana Costa', '99988877766', '02-14-1990', 7, 1,
        1, '11223344556', '', 'Enfermeira', 2, 
        'Enfermeira', 1, 3500.00, '445566778', 2), 

       ('Larissa Melo', '88877766655', '11-30-1992', 7, 1,
        1, '66554433221', '', 'Professora', 2,
        'Professora', 5, 3200.00, '112233445', 2), 

       ('Marcos Pereira', '77766655544', '09-10-1985', 5, 5, 
        1, '99887766554', '', 'Mecânico', 1,
        'Mecânico', 2, 2200.00, '223344556', 1),

       ('Felipe Pereira', '66655544433', '05-20-2016', 2, 7, 
        0, '33445566778', '', 'Estudante', 0,
        'Estudante', 2, 0.00, '998877665', 1);
    
INSERT INTO Familias(ConfiguracaoFamiliar)
VALUES (1),
       (0),
       (1),
       (2),
       (15), 
       (16), 
       (7);  

INSERT INTO Prontuarios(Codigo, CrasId, DataCriacao, FamiliaId, FormaDeAcesso, TecnicoId)
VALUES (1, 1, '03-11-2025', 1, 'Testando', 1),
       (2, 2, '04-10-2025', 2, 'Testando2', 2),
       (3, 3, '12-09-2025', 3, 1, 3), 
       (4, 5, '12-10-2025', 4, 2, 4),
       (5, 1, '12-11-2025', 7, 5, 5), 
       (6, 2, '12-12-2025', 8, 1, 6), 
       (7, 4, '12-12-2025', 9, 8, 1);

INSERT INTO TecnicoCras(CrasId, DataEntrada, TecnicoId, Admin)
VALUES (1, '04-11-2023', 1, 1),
       (2, '10-16-2022', 2, 1),
       (3, '01-15-2024', 3, 0), 
       (5, '02-20-2024', 4, 1); 

INSERT INTO FamiliaUsuarios(Familiaid, Parentesco, UsuarioId, DataAdicao)
VALUES ( 1, 1, 1, '12-08-2025'),
       ( 1, 2, 2, '12-05-2025'),
       
       (2, 1, 3, '12-08-2025'),
       
       (3, 1, 4, '12-09-2025'), 
       (3, 2, 5, '12-09-2025'), 
       (3, 3, 6, '12-09-2025'),
       
       (4, 1, 7, '12-10-2025'), 
       (4, 3, 8, '12-10-2025'),
       
       (5, 1, 9, '12-11-2025'), 

       (6, 1, 10, '12-12-2025'), 
       (6, 2, 11, '12-12-2025'), 

       (7, 1, 12, '12-12-2025'), 
       (7, 3, 13, '12-12-2025'); 
