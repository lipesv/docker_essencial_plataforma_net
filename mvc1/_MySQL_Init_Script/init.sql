CREATE DATABASE `produtosdb`;

use produtosdb;

DROP TABLE IF EXISTS `Produtos`;

CREATE TABLE
    `Produtos` (
        `ProdutoId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
        `Nome` VARCHAR(80) NOT NULL,
        `Categoria` VARCHAR(50) NOT NULL,
        `Preco` DECIMAL(10, 2) NOT NULL,
        PRIMARY KEY `pk_ProdutoId` (`ProdutoId`)
    ) ENGINE = InnoDB DEFAULT CHARSET = utf8;

LOCK TABLES `Produtos` WRITE;
INSERT INTO `Produtos` VALUES (1, 'Caneta', 'Material Escolar', '6.50');
INSERT INTO `Produtos` VALUES (2, 'Estojo', 'Material Escolar', '3.40');
INSERT INTO `Produtos` VALUES (3, 'Borracha', 'Material Escolar', '2.50');
UNLOCK TABLES;