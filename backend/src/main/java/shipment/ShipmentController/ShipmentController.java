package ShipmentController;

import shipment.dto.ShipmentDto;
import shipment.service.ShipmentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
@RestController // ← Sempre JSON (API)
@RequestMapping
@CrossOrigin(origins = "https://localhost:4200") // ← Angular sem CORS error

public class ShipmentController {

    @Autowired // ← Spring injeta Service automaticamente
    private ShipmentService shipmentService;

    @GetMapping
    public ResponseEntity<List<ShipmentDto>> getAllShipments() {
        return ResponseEntity.ok(shipmentService.getAllShipments ());
    }

}
