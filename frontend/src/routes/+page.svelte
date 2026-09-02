<script lang="ts">
	import { cart } from '$lib/cart.svelte';
	import { SvelteMap } from 'svelte/reactivity';

	interface Category {
		id: number;
		name: string,
	}

	interface MenuItem {
		id: number;
		name: string;
		description: string | null;
		price: number;
		isAvailable: boolean;
		imageUrl: string | null;

		categoryId: number;
		category: Category | null;
	}

	let menuItems = $state<MenuItem[]>([]);
	let loading = $state(true);
	let error = $state<string | null>(null);

	async function loadMenuItems(){
		try{
			const response = await fetch('https://localhost:5072/api/MenuItems');
			if(!response.ok){
				throw new Error('Fehler beim Laden der Speisekarte');
			}
			menuItems = await response.json();
		}catch(err){
			error = err instanceof Error ? err.message : 'Unbekannter Fehler';
		}finally{ // wird immer ausgeführt. Egal ob es funktioniert oder nicht.
			loading = false;
		}
	}

	loadMenuItems();
	
	// Gerichte nach Kategorien Gruppieren
	let groupedItems = $derived.by(() => {
		const groups = new SvelteMap<string, MenuItem[]>();

		for(const item of menuItems){
			const categoryName = item.category?.name ?? 'Ohne Kategorie';
			if (!groups.has(categoryName)){
				groups.set(categoryName, []);
			}
			groups.get(categoryName)!.push(item);
		}
		return groups;
	});

	
</script>