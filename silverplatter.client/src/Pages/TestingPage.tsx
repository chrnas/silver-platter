import { useEffect, useState } from "react";
import type { MenuItem } from "../Types/MenuItem";
import { menuEntryService } from "../service/MenuEntryService";
import { restaurantService } from "../service/RestaurantService";
import type { Restaurant } from "../Types/Restaurant";

function TestingPage() {
    const [entries, setEntries] = useState<Restaurant[]>([]);
    const [entry, setEntry] = useState<Restaurant>();

    let tempRestaurant = {
        id: 7,
        name: "Temporary Restaurant",
        description: "testing testing testing",
        address: "tempytemptemptemp"
    }

    useEffect(() => {
        restaurantService.getAll().then(data => {
            setEntries(data);
        }).catch(console.error);

        restaurantService.getById(7).then(data => {
            console.log("API response:", data);
            setEntry(data)}).catch(console.error);
        
        restaurantService.update(tempRestaurant);
        restaurantService.getById(7).then(data => {
            setEntry(data)
        }).catch(console.error);

        restaurantService.delete(8);
        }, []);

        
    
    return (
        <main className="TestingPage">
            <div className="content">
                {entry && (
                    <p>
                        {entry.name} - {entry.description}
                    </p>
                )}
                <div></div>
                {entries.map(e => ( 
                    <p key={e.id}> 
                    {e.name} - {e.description}  
                    </p> 
                ))}
            </div>
        </main>
    )
}

export default TestingPage;