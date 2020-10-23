# Planos API

Web API para controle de planos de telefonia.

A aplicação assume que já existe uma base com os dados de sustentação como operadora, DDD e tipo de plano. Para simularmos o uso da API, criamos uma carga de teste inicial com alguns desses dados (numa situação real, tais dados seriam consumidos ou acessados pela nossa API).  Logo, o foco da API é o CRUD do plano de telefonia.

A API foi construída em .NET Core usando o in-memory database. Como fazemos uso do Entity, será simples utilizá-la com outra base de dados. A API faz uso do OData (Open Data Protocol),  um protocolo que agiliza a criação e flexibiliza o consumo de serviços RESTful (possibilitando que filtros e paginação sejam implementos pelo frontend   pelo . Importante, a API está configurada para rodar em container para LINUX.

# Principais Serviços

**PLANO**
| Serviço| Ação|
| -- |--|
| GET /api/plano | Obter todos os planos de telefonia. |
| GET /api/plano/{id} | Obter plano por ID. |
| POST /api/plano/ | Criar novo plano. |
| PUT /api/plano/{id} | Atualizar um plano existente. |
| DELETE/api/plano/{id} | Excluir plano. |
| GET /api/plano/ddd/{ddd} | Obter planos por DDD. 

**Operadora**
| Serviço| Ação|
| -- |--|
| GET /api/operadora | Obter todos as operadoras de telefonia. |

**DDD**
| Serviço| Ação|
| -- |--|
| GET /api/ddd | Obter todos os DDD's. |

**Tipo de Plano**
| Serviço| Ação|
| -- |--|
| GET /api/tipoplano | Obter todos os tipos de plano de telefonia. |



## Utilização

Métodos que aceitam filtros por OData:
| Plano | Operadora |
|--|--|
| GET /api/plano | GET /api/operadora  |
| GET /api/plano/ddd/{ddd} | - |


Exemplos de consulta com OData ([tutorial](https://www.odata.org/getting-started/basic-tutorial/)):

=> Consultar todos os planos pré-pagos
 https://<< SuaURL >>/api/plano?$filter=idtipoplano eq 2

=> Consultar todos os planos com DDD 021 da operadora XYZ
https://<< SuaURL >>/api/plano/ddd/021?$filter=idoperadora eq 1

=> Consultar operadora com nome "Vivo"
https://<< SuaURL >>/api/operadora?$filter=nome eq 'Vivo' 

Para mais exemplos de consumo do serviço, consulte o arquivo "Utilizacao.pdf" no repositório.


## Container
API está configurada para rodar em container para LINUX. 
1 - Gere a imagem do container (docker build)
2 - Rode o docker (Ex.: docker run --rm -it -p 5000:80 planoapi:latest)
3 - Acessar a API pelo browser (Ex.: http://<< IP virtual machine >>:5000/api/plano)



