Для запуска возможно необходимо переустановить EF в обоих сборках. (через NUGET MANAGER)
Также возможно необходимо будет пересоздать локалку. (хотя врядли так как название базы я поменял)

Console Package Manager:

sqllocaldb stop

sqllocaldb delete

enable-migrations

add-migration Initial

update-database [-verbose]