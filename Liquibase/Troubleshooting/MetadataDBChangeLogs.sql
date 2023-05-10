--liquibase formatted sql

--changeset gina.s:create-product-catalog-table labels:product_catalog sqlCheck:SELECT count(1) FROM sku.sku_tracking where activated = 'true'
--comment Product Catalog table is dependent on sku tracking being activated.
CREATE TABLE product_catalog_tbl (
    id int primary key,
    product varchar(50) not null,
    sku varchar(20) not null,
    description varchar(50) not null,
    quantity int,
    cost float,
    category varchar(20) not null
)
--rollback drop table product_catalog_tbl

--changeset gina.s:insert-sample-data-product-catalog-table labels:product_catalog context:QA
--comment This data is meant for QA environment only. Do not run in production.
INSERT INTO product_catalog_tbl
    (id, product, sku, description, quantity, cost, category)
        VALUES
    (1, 'Item1', '123456', 'Test Item1 Description', 4, '50.99', 'CAT4')
-- rollback DELETE FROM product_catalog_tbl WHERE id = 1

--changeset gina.s:create-product-vendor-table labels:ALL
CREATE TABLE product_vendor_tbl (
    id int primary key,
    vendor varchar(50) not null,
    address1 varchar(50) not null,
    city varchar(30) not null,
    stateOrProvince varchar(3) not null,
    postalCode varchar(10) not null,
    country varchar(2) not null
)
--rollback drop table product_vendor_tbl