package com.example.kremlakova_repair_of_quipment;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class MainActivity extends AppCompatActivity {

    EditText editTextText, editTextTextPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }
    public void onClick_ok(View v) {
        String login = editTextText.getText().toString().trim();
        String password = editTextTextPassword.getText().toString().trim();

        if ("client1".equals(login) && "15995".equals(password)) {
            Intent intent = new Intent(MainActivity.this, MainActivity2.class);
            intent.putExtra("USER", "CLIENT");
            startActivity(intent);
        } else if ("admin".equals(login) && "11111".equals(password)) {
            Intent intent = new Intent(MainActivity.this, MainActivity2.class);
            intent.putExtra("USER", "ADMIN");
            startActivity(intent);
        } else {
            Toast.makeText(MainActivity.this, "Ошибка: неверный логин или пароль! Проверьте корректность данных!", Toast.LENGTH_SHORT).show();
        }
        Intent intent = new Intent(MainActivity.this, MainActivity2.class);
        intent.putExtra("complite", "1");
        startActivity(intent);
    }
}