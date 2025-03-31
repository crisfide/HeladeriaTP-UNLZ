let table = new DataTable('#tablaDatos');


document.getElementById("selectSabor").addEventListener("change", e => {
    let idHelado = document.getElementById("selectSabor").value

    let kilos = fetch("/Pedido/ObtenerKilos/" + idHelado)
        .then(res => res.text())
        .then(data => mostrarKilos(data))
        .catch(error => console.log(error))


})

const mostrarKilos = kilos => {
    document.getElementById("kilosDisponibles").classList.remove("d-none")
    document.getElementById("kilosDisponiblesValor").textContent = kilos
}