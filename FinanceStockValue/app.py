from flask import Flask, request, jsonify
from flask_cors import CORS  # Importa CORS
import yfinance as yf

app = Flask(__name__)
CORS(app)  # Abilita CORS per tutte le richieste

@app.route("/stock", methods=["GET"])
def get_stock_value():
    # Ottieni il simbolo dell'azione (ticker) dal parametro della query
    symbol = request.args.get("symbol", "")
    
    if not symbol:
        return jsonify({"error": "Nessun simbolo di azione fornito"}), 400
    
    # Ottieni i dati storici del titolo con yfinance
    from curl_cffi import requests
    session = requests.Session(impersonate="chrome")
    stock = yf.Ticker(symbol, session=session)
    stock_info = stock.history(period="1d")  # Dati per l'ultimo giorno
    
    if stock_info.empty:
        return jsonify({"error": "Simbolo azionario non valido o non trovato"}), 404
    
    # Prendi il valore di chiusura dell'ultimo giorno di trading
    price = stock_info['Close'].iloc[-1]
    
    return jsonify({
        "Symbol": symbol,
        "Price": price
    })

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=50000)  # Porta 50000