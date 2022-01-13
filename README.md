# Service Worker Integracao Jira

### Observações importantes:

Este projeto possui 4 Consumer do RabbitMq e um WorkerService dentro da mesma solution.

É possivel segregar os consumers. 
É possivel criar um serviço do windows com o WorkerService e rodar em background.


### Comando para criação do container Docker RabbitMQ:

docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management

### Commando para criação do container Docker MongoDb:

docker run --name meu_mongo -p 27018:27017 -d mongo

### Download Interface visual MongoDb Compass
https://www.mongodb.com/products/compass

![image](https://user-images.githubusercontent.com/31323149/147807383-6e0f3899-2378-4e72-9337-7b2f29fe5f22.png)



### observação: Fazer a configuração de multiplos startup, conforme imagem em anexo:
![image](https://user-images.githubusercontent.com/31323149/147634778-9df10dd0-8f8f-421b-8efd-2e5c6d3fa8a3.png)


### ** Deverá colocar a string de conexão no arquivo appSettings.json

![image](https://user-images.githubusercontent.com/31323149/147807147-a988bc6f-d9b6-4150-8663-8bc3b69a56f8.png)




### Documentação Jira:

https://developer.atlassian.com/cloud/jira/software/rest/intro/
https://developer.atlassian.com/cloud/jira/platform/rest/v3/intro/
https://developer.atlassian.com/server/jira/platform/jira-rest-api-examples/
https://www.atlassian.com/blog/jira-software/jql-the-most-flexible-way-to-search-jira-14
https://support.atlassian.com/jira-software-cloud/docs/advanced-search-reference-jql-fields/
https://www.atlassian.com/blog/jira-software/jql-the-most-flexible-way-to-search-jira-14
