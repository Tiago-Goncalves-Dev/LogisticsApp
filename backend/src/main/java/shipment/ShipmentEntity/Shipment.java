package ShipmentEntity;

import jakarta.persistence.*;
import java.time.LocalDateTime;


@Entity // Esta classe é uma tabela na DB
@Table(name = "shipments") // Nome da Tabela

public class Shipment {

    @Id // Primary Key
    @GeneratedValue(strategy = GenerationType.IDENTITY) //Auto-Increment
    private Long id;

    @Column(nullable = false) // Campo obrigatório na BD
    private String origin;

    @Column
    private String destination;

    @Enumerated(EnumType.STRING) //Salva enum como texto
    private ShipmentStatus status = ShipmentStatus.PENDING;

    private LocalDateTime createdAt;
    private LocalDateTime updatedAt;

    //Construtor vazio (JPA precisa)
    public Shipment() {}

    // Construtor útil
    public Shipment(String origin, String Destination) {
        this.origin = origin;
        this.destination = destination;
        this.createdAt = LocalDateTime.now();
        this.updatedAt = LocalDateTime.now();
    }
}
