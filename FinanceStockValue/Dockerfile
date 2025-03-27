# Usa un'immagine Python
FROM python:3.9

# Imposta la directory di lavoro
WORKDIR /app

# Copia i file del progetto
COPY . .

# Installa Flask e yfinance
RUN pip install flask yfinance flask-cors

# Espone la porta 50000
EXPOSE 50000

# Comando per avviare il server
CMD ["python", "app.py"]