# TCPSERVER
Debugar modo https ou http

# TCP CLIENT
Execute o comando a seguir no Shell:

```sh

# Definindo o endereço IP e porta do servidor
$server = "127.0.0.1"
$port = 22

try {
    # Criando um novo cliente TCP e conectando ao servidor
    $tcpClient = New-Object System.Net.Sockets.TcpClient($server, $port)
    $stream = $tcpClient.GetStream()
    
    # Criando um escritor de stream para enviar a mensagem
    $writer = New-Object System.IO.StreamWriter($stream)
    $writer.AutoFlush = $true
    
    # Enviando a mensagem para o servidor
    $message = "Hello from PowerShell"
    $writer.WriteLine($message)
    Write-Output "Sent: $message"
    
    # Lendo a resposta do servidor
    $reader = New-Object System.IO.StreamReader($stream)
    $response = $reader.ReadLine()
    Write-Output "Received: $response"
    
    # Fechando o stream e o cliente
    $writer.Close()
    $reader.Close()
    $tcpClient.Close()
} catch {
    Write-Error "An error occurred: $_"
}

```