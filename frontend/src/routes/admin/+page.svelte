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
</script>