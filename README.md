# API-Rest-ASPNET-CORE

<h1>ROTAS E REGRAS</h1>

<pre>
--------------------------------------------------------------------------------------------------------------------------------


* Inserir *
* Método HTTP: POST

/api/Lyfr/Inserir/

*ENVIAR MODEL PESSOA SERIALIZADO NO CONTENT*

FORMATO JSON:

{
	"Nome":"Lucas",
	"Senha":"123321",
	"Idade":18,
	"Email":"a@eu.com"
}

* Enviar todos os dados completos, menos os ID, porque ele ja cria automaticamente com o AUTO_INCREMENT
* Caso o email ja esteja registrado ele vai retornar erro porque o campo email está como UNIQUE

--------------------------------------------------------------------------------------------------------------------------------


* Alterar *
* Método HTTP: PUT

/api/Lyfr/Alterar/

*ENVIAR MODEL PESSOA SERIALIZADO NO CONTENT*

{
	"Id": 1
	"Nome":"Lucas",
	"Senha":"123321",
	"Idade":18,
	"Email":"a@eu.com"
}

* Enviar todos os dados completos, PRINCIPALMENTE ID POIS É COM O ID QUE ELE LOCALIZA QUEM ATUALIZAR
* Enviar ate mesmo os dados que não for atualizar do usuario, pois ele troca TODOS OS CAMPOS NO BANCO
* Então se voce for alterar somente o nome, por exemplo, terá que mandar o id, idade, email, senha anterior e o novo nome
* Caso contrário ira gerar um erro ou corromper o dado

--------------------------------------------------------------------------------------------------------------------------------

* Deletar *
* Método HTTP: DELETE

/api/Lyfr/Alterar/Deletar/

*ENVIAR O ID NO CONTENT*

* Deleta o usuário com o id informado
* Caso o usuário não existe retorna uma mensagem de erro

--------------------------------------------------------------------------------------------------------------------------------

* GetTodosUsuarios *
* Método HTTP: GET

* GetUsuarioByEmail *
* Método HTTP: POST

* GetUsuarioById *
* Método HTTP: POST

/api/Lyfr/GetTodosUsuarios

* Não precisa passar parâmetro nenhum, irá retornar todos os usuários do banco em json
* Exemplo:
[
	{
		"Id":3,
		"Nome":"Lucas",
		"Senha":"123321",
		"Idade":18,
		"Email":"a@eu1.com"
	},

	{
		"Id":4,
		"Nome":"Gabriel",
		"Senha":"321123",
		"Idade":22,
		"Email":"gabriel@eu.com"
	}
]


/api/Lyfr/GetUsuarioByEmail/

*ENVIAR EMAIL NO CONTENT*

* Irá retornar um usuário que será pego pelo email
* Caso o email não exista retornará uma mensagem de erro avisando
* Exemplo: 

{
	"Id":3,
	"Nome":"Lucas",
	"Senha":"123321",
	"Idade":18,
	"Email":"a@eu1.com"
}


/api/Lyfr/GetUsuarioById/

*ENVIAR ID NO CONTENT*

* Irá retornar um usuário que será pego pelo id
* Caso o id não exista retornará uma mensagem de erro avisando
* Exemplo: 

{
	"Id":4,
	"Nome":"Gabriel",
	"Senha":"321123",
	"Idade":22,
	"Email":"gabriel@eu.com"
}

--------------------------------------------------------------------------------------------------------------------------------
</pre>
