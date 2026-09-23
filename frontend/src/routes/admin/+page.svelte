<script lang="ts">
    interface MenuItem{
        id: number;
        name: string;
    }

    interface OrderItem{
        id: number;
        quantity: number;
        unitPrice: number;
        note: string|null;
        menuItemId: number;
        menuItem: MenuItem | null;
    }

    interface Table{
        id: number;
        tableNumber: number;
        seats: number;
    }

    interface Order{
        id: number;
        createAt: string;
        status: number;
        tableId: number;
        table: Table | null;
        oderItems: OrderItem[];
    }

    const statusLabels: Record<number, string> = {
        0: 'Offen',
        1: 'In Zubereitung',
        2: 'Serviert',
        3: 'Bezahlt',
    }

    let orders = $state<order[]>([]);
    let loading = $state(true);
    let error = $state<string | null>(null);

    async function loadOrders(){
        loading = true;
        error = null;
        try{
            const response = await fetch('http://localhost:5072/api/Orders');
            if(!response.ok){
                throw new Error('Fehler beim laden der Bestellungen.');
            }
            orders = await response.json();
        }catch(err){
            error = err instanceof Error ? err.message : 'Unbekannter Fehler';
        }finally{
            loading = false;
        }
    }

    async function updateStatus(order: Order, newStatus: number){
        try{
            const orderId = order.id;
            const response = await fetch(`https://localhost:5072/api/Orders/${orderId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    tableId: order.tableId,
                    status: newStatus,
                    createtAt: order.createAt,
                })
            });

            if(!response.ok){
                throw new Error('Fehler beim Aktualisieren des Status');
            }

            order.status = newStatus;
        }catch(err){
            error = err instanceof Error ? err.message : 'Unbekannter Fehler';
        }
    }

    function calculateTotal(order: Order): number{
        return order.oderItems
                .reduce((sum, item) => sum + item.unitPrice * item.quantity, 0);
    }

    loadOrders();
</script>

<main>
    <h1>Bestellungen</h1>

    <button class="refresh" onclick={loadOrders}>Aktualisieren</button>

    {#if loading}
        <p>Ladet...</p>
    {:else if error}
        <p class="error">{error}</p>
    {:else if orders.length === 0}
        <p>Keine Bestellungen vorhanden.</p>
    {:else}
        <div class="orders-list">
            {#each orders as order (order.id)}
                <div class="order-card">
                    <div class="order-header">
                        <span class="table-info">
                            Tisch {order.table?.tableNumber ?? order.tableId}
                        </span>
                        <select 
                            value={order.status}
                            onchange={(e) => updateStatus(order, Number(e.currentTarget.value))}
                        >
                            {#each Object.entries(statusLabels) as [value, label] (value)}
                                <option value={value}>{label}</option>
                            {/each}
                        </select>
                    </div>
                </div>
            {/each}
        </div>
    {/if}
</main>