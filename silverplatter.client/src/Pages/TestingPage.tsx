import { useEffect, useState } from "react";
import type { MenuItem } from "../Types/MenuItem";
import { menuEntryService } from "../service/MenuEntryService";

function TestingPage() {
    const [entries, setEntries] = useState<MenuItem[]>([]);

    useEffect(() => {
        menuEntryService.getAll().then(data => {
      console.log("API response:", data);
      setEntries(data)}).catch(console.error);
    }, []);

    return (
        <main className="TestingPage">
            <div className="content">
                {entries.map(e => (
                    <p key={e.id}>
                        {e.name} - {e.description} 
                        {/**Gives errors since the retrieved items from database is lower case
                         * and the elements in MenuItem is upper case  */ } 
                        
                    </p>
                ))}
            </div>
        </main>
    )
}

export default TestingPage;