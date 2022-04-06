# ToggAssessment

## Gereksinimler

RabbitMQ ve PostgreSQL'i docker desktop üzerine kurulum gerçekleştirdim.

 - RabbitMQ
 - PostgreSQL
 - .Net 6




## Kurulum

Kurulum işlemleri için "UserPanel.Data" 'da ve "ManagementPanel.Data" 'da migrate işlemleri gerekmektedir.

    cd ~/ProjectFolder/ToggAssessment/UserPanel.Data
    dotnet ef database update
    
    cd ~/ProjectFolder/ToggAssessment/ManagementPanel.Data
    dotnet ef database update
    



## Connection String Ayarlamaları

ManagementPanel.Consumer, ManagementPanel.GrpcUI, UserPanel.Consumer ve UserPanel.API altında appsettings.json dosyasından Postgresql ve/veya RabbitMQ bağlantı bilgilerini değiştiriniz.



## Projenin Başlatılması

Projenin başlatılması için Solution üzerinden Set Startup Project üzerinden aşağıdaki ayarlamaların gerçekleştirilmesi gerekmektedir.

![image](https://user-images.githubusercontent.com/32467049/162075998-f775f979-4d43-4191-8859-d052aa5b1284.png)
